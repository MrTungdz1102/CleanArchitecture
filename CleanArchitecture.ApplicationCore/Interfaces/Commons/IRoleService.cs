using CleanArchitecture.ApplicationCore.Commons;

namespace CleanArchitecture.ApplicationCore.Interfaces.Commons
{
    public interface IRoleService
    {
        Task<bool> AssignRole(string email, string[] roleName);
        ResponseDTO GetAllRole();
        Task<ResponseDTO> GetAllUserRoleAsync(string userId);
    }
}
