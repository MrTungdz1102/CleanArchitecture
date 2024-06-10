using AutoMapper;
using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities.DTOs;
using CleanArchitecture.ApplicationCore.Interfaces.Commons;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private ResponseDTO _response;
        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
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
    }
}
