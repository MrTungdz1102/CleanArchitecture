using CleanArchitecture.WebUI.Models.DTOs;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ResponseDTO?> GetAllRole();
        Task<ResponseDTO?> GetAllUserRoleAsync(string userId);
    }
}
