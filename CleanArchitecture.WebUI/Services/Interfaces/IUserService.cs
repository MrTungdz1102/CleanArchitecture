using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDTO?> GetAllUserAsync();
        Task<ResponseDTO?> LockUnlockAsync(string userId);
        Task<ResponseDTO?> GetUserInfoAsync(string userId);
        Task<ResponseDTO?> UpdateUserAsync(AppUser user);
        Task<ResponseDTO?> ForgotPasswordAsync(string email);
        Task<ResponseDTO?> ResetPasswordAsync(ResetPasswordDTO passwordDTO);
        Task<ResponseDTO?> ChangePasswordAsync(ChangePasswordDTO passwordDTO);
    }
}
