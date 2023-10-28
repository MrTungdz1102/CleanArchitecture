using CleanArchitecture.WebUI.Models.ViewModel;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface ITokenProvider
    {
        void SetToken(string token, string refreshToken, string refreshTokenExpires);
        string? GetToken();
        void ClearToken();
      //  string CallRefreshToken(AppUserVM appUserVM);
    }
}
