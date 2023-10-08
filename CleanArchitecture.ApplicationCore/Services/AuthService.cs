using AutoMapper;
using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private ResponseDTO _response;
        private AppUser? _user;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        public AuthService(UserManager<AppUser> userManager, IJwtTokenGenerator tokenGenerator, IMapper mapper, IRoleService roleService)
        {
            _userManager = userManager;
            _response = new ResponseDTO();
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
            _roleService = roleService;

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
                    _response.IsSuccess = false;
                    _response.Message = "Username or password is incorrect";
                    return _response;
                }
                else
                {
                    var token = await _tokenGenerator.GenerateToken(_user);
                  //  LoginResponseDTO loginResponse = new LoginResponseDTO { appUser = _user, Token = token };
                    _response.Result = token;
                    return _response;
                }
            }
            catch(Exception ex)
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
                _user = _mapper.Map<AppUser>(registerRequest);
                _user.CreateTime = DateTime.Now;
                _user.UserName = registerRequest.Email;
                IdentityResult result = await _userManager.CreateAsync(_user, registerRequest.Password);
                if (result.Succeeded)
                {
                    if (registerRequest.Roles is null || registerRequest.Roles.Length == 0)
                    {
                        await _roleService.AssignRole(registerRequest.Email, new string[] {"USER", "CUSTOMER"});
                    }
                    else
                    {
                        await _roleService.AssignRole(registerRequest.Email, registerRequest.Roles);
                    }
                    
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = string.Join("; ", result.Errors.Select(error => $"{error.Code}: {error.Description}"));
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
