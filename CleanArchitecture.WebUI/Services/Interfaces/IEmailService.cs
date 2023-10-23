using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface IEmailService
    {
        Task<ResponseDTO?> SendEmailAsync(EmailVM email);
    }
}
