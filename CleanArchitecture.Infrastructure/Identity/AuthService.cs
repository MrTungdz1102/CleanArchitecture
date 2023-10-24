using AutoMapper;
using Azure.Core;
using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

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
        public AuthService(UserManager<AppUser> userManager, IJwtTokenGenerator tokenGenerator, IRoleService roleService, IAppLogger<AppUser> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _response = new ResponseDTO();
            _tokenGenerator = tokenGenerator;
            _roleService = roleService;
            _logger = logger;
            _configuration = configuration;
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
                    var token = await _tokenGenerator.GenerateToken(loginRequest.Email);
                    LoginResponseDTO loginResponse = new LoginResponseDTO
                    {
                        Id = _user.Id,
                        Name = _user.Name,
                        Email = _user.Email,
                        PhoneNumber = _user.PhoneNumber,
                        Token = token,
                        RefreshToken = await _tokenGenerator.CreateRefreshToken(_user.Email),
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
                string refreshToken = await _tokenGenerator.VerifyRefreshToken(loginResponseDTO);
                if(!string.IsNullOrEmpty(refreshToken))
                {
                    var token = await _tokenGenerator.GenerateToken(loginResponseDTO.Email);
                    LoginResponseDTO loginResponse = new LoginResponseDTO
                    {
                        Token = token,
                        RefreshToken = await _tokenGenerator.CreateRefreshToken(loginResponseDTO.Email),
                        RefreshTokenExpire = DateTime.Now.AddMinutes(int.Parse(_configuration["RefreshToken:ExpiresDay"]))
                    };
                    _response.Result = loginResponse;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "An error occur when verify token";
                    _logger.LogWarning("An error occur when verify token");
                }  
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
