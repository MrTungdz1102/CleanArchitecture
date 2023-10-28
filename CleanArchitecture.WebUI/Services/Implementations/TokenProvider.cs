using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CleanArchitecture.WebUI.Services.Implementations
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _accessor;
        public TokenProvider(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        //public string CallRefreshToken(AppUserVM appUserVM)
        //{
        //    appUserVM = new AppUserVM
        //    {
        //        Id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
        //        Token = _accessor.HttpContext.Request.Cookies[$"{Constants.TokenCookie}"],
        //        RefreshToken = 
        //    }
        //}

        public void ClearToken()
        {
            _accessor.HttpContext?.Response.Cookies.Delete(Constants.TokenCookie);
            _accessor.HttpContext?.Response.Cookies.Delete(Constants.RefreshToken);
        }

        public TokenDTO? GetToken()
        {
            //string? token = null;
            //bool? hasToken = _accessor.HttpContext?.Request.Cookies.TryGetValue(Constants.TokenCookie, out token);
            //return hasToken is true ? token : null;
            try
            {
                bool hasAccessToken = _accessor.HttpContext.Request.Cookies.TryGetValue(Constants.TokenCookie, out string accessToken);
                bool hasRefreshToken = _accessor.HttpContext.Request.Cookies.TryGetValue(Constants.RefreshToken, out string refreshToken);

                TokenDTO tokenDTO = new()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
                return hasAccessToken ? tokenDTO : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void SetToken(string token, string refreshToken, string refreshTokenExpires)
        {
            CookieOptions cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Parse(refreshTokenExpires)
            };
            _accessor.HttpContext?.Response.Cookies.Append(Constants.TokenCookie, token);
            _accessor.HttpContext?.Response.Cookies.Append(Constants.RefreshToken, refreshToken, cookieOptions);
         //   _accessor.HttpContext?.Response.Cookies.Append(Constants.RefreshTokenExpires, refreshTokenExpires);
        }
    }
}
