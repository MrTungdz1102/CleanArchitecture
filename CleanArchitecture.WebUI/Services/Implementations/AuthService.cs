using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;
using NuGet.Protocol.Plugins;

namespace CleanArchitecture.WebUI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> Login(LoginVM loginRequest)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/AuthAPI/Login",
                ApiType = Utilities.Constants.ApiType.POST,
                Data = loginRequest
            });
        }

        public async Task<ResponseDTO?> Register(RegisterVM registerRequest)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/AuthAPI/Register",
                ApiType = Utilities.Constants.ApiType.POST,
                Data = registerRequest
            });
        }
    }
}
