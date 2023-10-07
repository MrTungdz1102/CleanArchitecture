using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;

namespace CleanArchitecture.WebUI.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IBaseService _baseService;

        public RoleService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> GetAllRole()
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/RoleApi/GetAllRole",
                ApiType = Constants.ApiType.GET
            });
        }
    }
}
