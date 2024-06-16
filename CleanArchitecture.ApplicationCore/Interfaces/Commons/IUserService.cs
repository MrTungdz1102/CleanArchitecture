using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities.DTOs;

namespace CleanArchitecture.ApplicationCore.Interfaces.Commons
{
    public interface IUserService
    {
        Task<int> GetAllQuantityUser();
        Task<int> CountUserCreateByTime(DateTime? startDate, DateTime? endDate);
        Task<List<AppUserDTO>> GetAllUser();

        Task<ResponseDTO> GetAllUserAsync();
        Task<ResponseDTO> LockUnlockAsync(string userId);
        Task<ResponseDTO> GetUserInfoAsync(string userId);
        Task<ResponseDTO> UpdateUserAsync(AppUserDTO userDTO);
        Task<ResponseDTO> ForgotPasswordAsync(string email);
        Task<ResponseDTO> ResetPasswordAsync(ResetPasswordDTO passwordDTO);
        Task<ResponseDTO> ChangePasswordAsync(ChangePasswordDTO passwordDTO);

    }
}
