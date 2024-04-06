using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Commons;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System.Diagnostics;

namespace CleanArchitecture.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        private readonly string? _sendMailKey;
        private ResponseDTO _response;
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
			_sendMailKey = _configuration["Sendinblue:Key"];
            _response = new ResponseDTO();
        }
        public async Task<ResponseDTO> SendEmailAsync(EmailDTO emailDTO)
        {
            try
            {
				Configuration.Default.ApiKey.Add("api-key", _sendMailKey);

				var apiInstance = new TransactionalEmailsApi();
				string SenderName = "Tung Dao";
				string SenderEmail = "iloveyoubaby112709@gmail.com";
				SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
				SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(emailDTO.Email);
				List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
				To.Add(smtpEmailTo);
				
				string? HtmlContent = null;
				string TextContent = emailDTO.Message;
				string Subject = emailDTO.Subject;

				var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, HtmlContent, TextContent, Subject);
				CreateSmtpEmail result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
			}
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
