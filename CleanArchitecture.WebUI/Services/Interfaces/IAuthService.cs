using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDTO?> Register(RegisterVM registerRequest);
        Task<ResponseDTO?> Login(LoginVM loginRequest);
    }
}
