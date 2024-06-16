using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;

namespace CleanArchitecture.WebUI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseService _baseService;
        public UserService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> ChangePasswordAsync(ChangePasswordDTO passwordDTO)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/UserAPI/ChangePassword",
                ApiType = Constants.ApiType.PUT,
                Data = passwordDTO
            });
        }

        public async Task<ResponseDTO?> ForgotPasswordAsync(string email)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/UserAPI/ForgotPassword",
                ApiType = Constants.ApiType.POST,
                Data = email
            });
        }

        public async Task<ResponseDTO?> GetAllUserAsync()
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/UserAPI/GetAllUser",
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> GetUserInfoAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/UserAPI/GetUserInfo?userId=" + userId,
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> LockUnlockAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/UserAPI/LockUnlockUser?userId=" + userId,
                ApiType = Constants.ApiType.PUT
            });
        }

        public async Task<ResponseDTO?> ResetPasswordAsync(ResetPasswordDTO passwordDTO)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/UserAPI/ResetPassword",
                ApiType = Constants.ApiType.POST,
                Data = passwordDTO
            });
        }

        public async Task<ResponseDTO?> UpdateUserAsync(AppUser user)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/UserAPI/UpdateUser",
                ApiType = Constants.ApiType.PUT,
                Data = user
            });
        }
    }
}
