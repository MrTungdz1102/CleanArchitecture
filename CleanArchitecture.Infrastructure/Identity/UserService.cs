using AutoMapper;
using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities.DTOs;
using CleanArchitecture.ApplicationCore.Interfaces.Commons;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailSender _email;
        private readonly IConfiguration _configuration;
        private ResponseDTO _response;
        public UserService(UserManager<AppUser> userManager, IMapper mapper, IConfiguration configuration, IEmailSender email)
        {
            _userManager = userManager;
            _mapper = mapper;
            _email = email;
            _configuration = configuration;
            _response = new ResponseDTO();
        }

        public async Task<int> CountUserCreateByTime(DateTime? startDate, DateTime? endDate)
        {
            List<AppUser> appUsers = await _userManager.Users.ToListAsync();
            return appUsers.Count(x => x.CreateTime >= startDate && x.CreateTime <= endDate);
        }

        public async Task<int> GetAllQuantityUser()
        {
            List<AppUser> appUsers = await _userManager.Users.ToListAsync();
            return appUsers.Count;
        }
        public async Task<List<AppUserDTO>> GetAllUser()
        {
            IEnumerable<AppUser> appUsers = await _userManager.Users.ToListAsync();
            List<AppUserDTO> appUserDTOs = new List<AppUserDTO>();
            foreach (var user in appUsers)
            {
                appUserDTOs.Add(AppUserMapper(user));
            }
            return appUserDTOs;
        }

        public AppUserDTO AppUserMapper(AppUser user)
        {
            return new AppUserDTO { Name = user.Name, CreateTime = user.CreateTime };
        }

        public async Task<ResponseDTO> GetAllUserAsync()
        {
            try
            {
                _response.Result = await _userManager.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> LockUnlockAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Can not find user";
                }
                else
                {
                    if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
                    {
                        user.LockoutEnd = DateTime.Now;
                    }
                    else
                    {
                        user.LockoutEnd = DateTime.Now.AddYears(100);
                    }
                    await _userManager.UpdateAsync(user);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetUserInfoAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Can not find out user";
                }
                else
                {
                    AppUser userInfo = new();
                    userInfo = user;
                    userInfo.Roles = await _userManager.GetRolesAsync(user);
                    _response.Result = userInfo;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> UpdateUserAsync(AppUserDTO userDTO)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userDTO.Id);
                if (user is null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Can not find out user";
                }
                else
                {
                    user.Name = userDTO.Name;
                    user.Email = userDTO.Email;
                    user.PhoneNumber = userDTO.PhoneNumber;
                    if (userDTO.Roles != null)
                    {
                        var oldRoles = await _userManager.GetRolesAsync(user);
                        await _userManager.RemoveFromRolesAsync(user, oldRoles);
                        await _userManager.AddToRolesAsync(user, userDTO.Roles);
                    }
                    await _userManager.UpdateAsync(user);
                    _response.Result = user;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> ForgotPasswordAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user is null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Can not find out user";
                }
                else
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    string host = _configuration.GetValue<string>("ClientUrl");
                    string encodeToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                    string resetPasswordUrl = $"{host}access/resetpassword?email={email}&token={encodeToken}";
                    string json = JsonConvert.SerializeObject(resetPasswordUrl);
                    _response.Result = json;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> ResetPasswordAsync(ResetPasswordDTO passwordDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(passwordDTO.Email);
                if (string.IsNullOrEmpty(passwordDTO.Token))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid Token";
                    return _response;
                }
                if (user is null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Can not find out user";
                }
                else
                {
                    string decodeToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(passwordDTO.Token));
                    var result = await _userManager.ResetPasswordAsync(user, decodeToken, passwordDTO.Password);

                    if (result.Succeeded)
                    {
                        _response.IsSuccess = true;
                        _response.Result = result;
                        return _response;
                    }

                    _response.IsSuccess = false;
                    _response.Message = result.Errors.ToList()[0].Description;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> ChangePasswordAsync(ChangePasswordDTO passwordDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(passwordDTO.Email);
                if (user is null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Can not find out user";
                }
                else
                {
                    var result = await _userManager.ChangePasswordAsync(user, passwordDTO.CurrentPassword, passwordDTO.NewPassword);
                    if (result.Succeeded)
                    {
                        _response.IsSuccess = true;
                        _response.Result = result;
                        return _response;
                    }
                    _response.IsSuccess = false;
                    _response.Message = result.Errors.ToList()[0].Description;
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
