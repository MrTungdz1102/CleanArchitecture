using CleanArchitecture.WebUI.Exceptions;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.WebUI.Services.Implementations
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _token;
        private readonly IHttpMessageRequest _httpMessageRequest;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider token, IHttpMessageRequest httpMessageRequest, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _token = token;
            _httpMessageRequest = httpMessageRequest;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO, bool withBearer = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("API");
                // func httprequestmessage
                var messageFactory = () =>
                {
                    return _httpMessageRequest.Build(requestDTO);
                };
                HttpResponseMessage? responseMessage = null;
                responseMessage = await SendWithRefreshTokenAsync(client, messageFactory, withBearer);
                switch (responseMessage.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    case HttpStatusCode.BadRequest:
                        return new() { IsSuccess = false, Message = "Bad Request" };
                    default:
                        var apiContent = await responseMessage.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDTO
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }
        }


        private async Task<HttpResponseMessage> SendWithRefreshTokenAsync(HttpClient httpClient,
            Func<HttpRequestMessage> httpRequestMessageFactory, bool withBearer = true)
        {
            if (!withBearer)
            {
                return await httpClient.SendAsync(httpRequestMessageFactory());
            }
            else
            {
                TokenDTO? tokenDTO = _token.GetToken();
                if (tokenDTO != null && !string.IsNullOrEmpty(tokenDTO.AccessToken))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenDTO.AccessToken);
                }
                try
                {
                    var response = await httpClient.SendAsync(httpRequestMessageFactory());
                    if (response.IsSuccessStatusCode)
                        return response;

                    // IF this fails then we can pass refresh token!
                    if (!response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        //GENERATE NEW Token from Refresh token / Sign in with that new token and then retry
                        await InvokeRefreshTokenEndpoint(httpClient, tokenDTO.AccessToken, tokenDTO.RefreshToken);
                        response = await httpClient.SendAsync(httpRequestMessageFactory());
                        return response;
                    }
                    return response;

                }
                catch (AuthException)
                {
                    throw;
                }
            }
        }

        private async Task InvokeRefreshTokenEndpoint(HttpClient httpClient, string existingAccessToken, string existingRefreshToken)
        {
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri($"{Constants.APIUrlBase}/api/AuthAPI/RefreshAccessToken");
            message.Method = HttpMethod.Post;
            message.Content = new StringContent(JsonConvert.SerializeObject(new TokenDTO()
            {
                AccessToken = existingAccessToken,
                RefreshToken = existingRefreshToken
            }), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.SendAsync(message);
            string content = await response.Content.ReadAsStringAsync();
            ResponseDTO? responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(content);
            if (responseDTO.Result is null || responseDTO?.IsSuccess != true)
            {
                await _httpContextAccessor.HttpContext.SignOutAsync();
                _token.ClearToken();
                throw new AuthException();
            }
            else
            {
                 var tokenDataStr = JsonConvert.SerializeObject(responseDTO.Result);
                var tokenDto = JsonConvert.DeserializeObject<TokenDTO>(responseDTO.Result.ToString());

                if (tokenDto != null && !string.IsNullOrEmpty(tokenDto.AccessToken))
                {
                    //New method to sign in with the new token that we receive
                    await SignInWithNewTokens(tokenDto);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenDto.AccessToken);
                }
            }
        }

        private async Task SignInWithNewTokens(TokenDTO tokenDTO)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(tokenDTO.AccessToken);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(x => x.Type == "role").Value));
            identity.AddClaim(new Claim(ClaimTypes.Email, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(ClaimTypes.MobilePhone, jwt.Claims.FirstOrDefault(x => x.Type == "PhoneNumber").Value));

            var principal = new ClaimsPrincipal(identity);
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            _token.SetToken(tokenDTO.AccessToken, tokenDTO.RefreshToken, tokenDTO.RefreshTokenExpire.ToString());
        }
    }
}
