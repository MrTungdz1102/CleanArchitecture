namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface ITokenProvider
    {
        void SetToken(string token, string refreshToken, string refreshTokenExpires);
        string? GetToken();
        void ClearToken();
    }
}
