using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface ITokenProvider
    {
        void SetToken(string token, string refreshToken, string refreshTokenExpires);
        TokenDTO? GetToken();
        void ClearToken();
      //  string CallRefreshToken(AppUserVM appUserVM);
    }
}
