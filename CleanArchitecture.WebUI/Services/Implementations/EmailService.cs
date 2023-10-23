using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;

namespace CleanArchitecture.WebUI.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IBaseService _baseService;

        public EmailService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> SendEmailAsync(EmailVM email)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = Utilities.Constants.ApiType.POST,
                Url = Constants.APIUrlBase + "/api/EmailAPI/SendEmail",
                Data = email
            });
        }
    }
}
