using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using CleanArchitecture.Infrastructure.Identity.DTOs;
using Microsoft.AspNetCore.Identity;

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

        public AuthService(UserManager<AppUser> userManager, IJwtTokenGenerator tokenGenerator, IRoleService roleService, IAppLogger<AppUser> logger)
        {
            _userManager = userManager;
            _response = new ResponseDTO();
            _tokenGenerator = tokenGenerator;
            _roleService = roleService;
            _logger = logger;
        }

        public async Task<ResponseDTO> Login(LoginRequestDTO loginRequest)
        {
            try
            {
                bool checkPass = false;
                _user = await _userManager.FindByEmailAsync(loginRequest.Email);

                if (_user != null)
                {
                    if (_user.LockoutEnd != null && _user.LockoutEnd > DateTime.Now)
                    {
                        _response.IsSuccess = false;
                        _response.Message = $"User has been locked to {_user.LockoutEnd}";
                        return _response;
                    }
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
                        AppUser = _user,
                        Token = token
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
                        await _roleService.AssignRole(registerRequest.Email, new string[] { "USER" });
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
    }
}
