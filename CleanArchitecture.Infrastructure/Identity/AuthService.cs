using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Identity;
using CleanArchitecture.ApplicationCore.Interfaces.Repositories;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using CleanArchitecture.ApplicationCore.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private ResponseDTO _response;
        private AppUser? _user;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IRoleService _roleService;
        private readonly IAppLogger<AppUser> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(UserManager<AppUser> userManager, IJwtTokenGenerator tokenGenerator, IRoleService roleService, IAppLogger<AppUser> logger, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _response = new ResponseDTO();
            _tokenGenerator = tokenGenerator;
            _roleService = roleService;
            _logger = logger;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> Login(LoginRequestDTO loginRequest)
        {
            try
            {
                bool checkPass = false;
                _user = await _userManager.FindByEmailAsync(loginRequest.Email);
                if (_user != null)
                {
                    checkPass = await _userManager.CheckPasswordAsync(_user, loginRequest.Password);
                }
                if (_user == null || checkPass == false)
                {
                    //  loginResponse = new LoginResponseDTO() { appUser = null, Token = "" };
                    _logger.LogWarning($"User with email {loginRequest.Email} was not found");
                    _response.IsSuccess = false;
                    _response.Message = "Username or password is incorrect";
                    return _response;
                }
                else
                {
                    var jwtTokenId = $"JTI{Guid.NewGuid()}";
                    var token = await _tokenGenerator.GenerateToken(loginRequest.Email, jwtTokenId);
                    LoginResponseDTO loginResponse = new LoginResponseDTO
                    {
                        Id = _user.Id,
                        Name = _user.Name,
                        Email = _user.Email,
                        PhoneNumber = _user.PhoneNumber,
                        Token = token,
                        RefreshToken = await CreateUserRefreshToken(_user.Id, jwtTokenId),
                        RefreshTokenExpire = DateTime.Now.AddDays(int.Parse(_configuration["RefreshToken:ExpiresDay"]))
                    };
                    _response.Result = loginResponse;
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> Register(RegisterRequestDTO registerRequest)
        {
            try
            {
                _user = new AppUser
                {
                    PhoneNumber = registerRequest.PhoneNumber,
                    Name = registerRequest.Name,
                    Email = registerRequest.Email,
                    CreateTime = DateTime.Now,
                    UserName = registerRequest.Email
                };
                IdentityResult result = await _userManager.CreateAsync(_user, registerRequest.Password);
                if (result.Succeeded)
                {
                    if (registerRequest.Roles is null || registerRequest.Roles.Length == 0)
                    {
                        await _roleService.AssignRole(registerRequest.Email, new string[] { "USER", "CUSTOMER" });
                    }
                    else
                    {
                        await _roleService.AssignRole(registerRequest.Email, registerRequest.Roles);
                    }

                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = string.Join(" ", result.Errors.Select(error => $"{error.Code}: {error.Description}"));
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> VerifyRefreshToken(LoginResponseDTO loginResponseDTO)
        {
            try
            {
                //string refreshToken = await _tokenGenerator.VerifyRefreshToken(loginResponseDTO);
                //if(!string.IsNullOrEmpty(refreshToken))
                //{
                //    var token = await _tokenGenerator.GenerateToken(loginResponseDTO.Email);
                //    LoginResponseDTO loginResponse = new LoginResponseDTO
                //    {
                //        Token = token,
                //     //   RefreshToken = await _tokenGenerator.CreateRefreshToken(loginResponseDTO.Email),
                //        RefreshTokenExpire = DateTime.Now.AddMinutes(int.Parse(_configuration["RefreshToken:ExpiresDay"]))
                //    };
                _response.Result = await _tokenGenerator.VerifyRefreshToken(loginResponseDTO);
                //}
                //else
                //{
                //    _response.IsSuccess = false;
                //    _response.Message = "An error occur when verify token";
                //    _logger.LogWarning("An error occur when verify token");
                //}  
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<string> CreateUserRefreshToken(string userId, string jwtTokenId)
        {
            UserRefreshToken userRefreshToken = new UserRefreshToken
            {
                IsValidRefreshToken = true,
                UserId = userId,
                JwtTokenId = jwtTokenId,
                ExpireTime = DateTime.Now.AddMinutes(int.Parse(_configuration["RefreshToken:ExpiresDay"])),
                RefreshToken = await _tokenGenerator.CreateRefreshToken(userId)
            };
            await _unitOfWork.userRefreshTokenRepo.AddAsync(userRefreshToken);
            return userRefreshToken.RefreshToken;
        }

        public async Task<ResponseDTO> RefreshAccessToken(LoginResponseDTO loginResponseDTO)
        {
            try
            {
                // Find an existing refresh token
                var specification = new UserRefreshTokenSpecification(loginResponseDTO.RefreshToken);
                UserRefreshToken? existingRefreshToken = await _unitOfWork.userRefreshTokenRepo.FirstOrDefaultAsync(specification);
                if (existingRefreshToken == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Not Found User's Refresh Token";
                    return _response;
                }
                // Compare data from existing refresh and access token provided and if there is any mismatch then consider it as a fraud
                bool isValidToken = _tokenGenerator.ValidateAccessToken(loginResponseDTO.Token, existingRefreshToken.UserId, existingRefreshToken.JwtTokenId);
                if (!isValidToken)
                {
                    _response.IsSuccess = false;
                    await DisableRefreshToken(existingRefreshToken);
                    return _response;
                }
                // When someone tries to use not valid refresh token, fraud possible
                if (!existingRefreshToken.IsValidRefreshToken)
                {
                    await DisableAllRefreshToken(existingRefreshToken.UserId, existingRefreshToken.JwtTokenId);
                }
                // If just expired then mark as invalid and return empty
                if (existingRefreshToken.ExpireTime < DateTime.UtcNow)
                {
                    _response.IsSuccess = false;
                    await DisableRefreshToken(existingRefreshToken);
                    _response.Message = "Refresh token was expire";
                    return _response;
                }
                // replace old refresh with a new one with updated expire date
                var newRefreshToken = await CreateUserRefreshToken(existingRefreshToken.UserId, existingRefreshToken.JwtTokenId);

                // revoke existing refresh token
                await DisableRefreshToken(existingRefreshToken);

                // generate new access token
                AppUser? user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == existingRefreshToken.UserId);
                if(user is null)
                {
                    _response.IsSuccess = false;
                     _response.Message = "Refresh token was expire";
                    return _response;
                }
                _response.Result = new LoginResponseDTO
                {
                    Token = await _tokenGenerator.GenerateToken(user.UserName, existingRefreshToken.JwtTokenId),
                    RefreshToken = newRefreshToken
                };
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        private async Task DisableRefreshToken(UserRefreshToken userRefreshToken)
        {
            userRefreshToken.IsValidRefreshToken = false;
            await _unitOfWork.userRefreshTokenRepo.UpdateAsync(userRefreshToken);
        }
        private async Task DisableAllRefreshToken(string userId, string tokenId)
        {
            await _unitOfWork.userRefreshTokenRepo.DisableAllRefreshToken(userId, tokenId);
        }
    }
}
