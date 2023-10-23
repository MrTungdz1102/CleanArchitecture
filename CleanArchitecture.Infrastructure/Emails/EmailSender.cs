using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Commons;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        private readonly string? _sendGridKey;
        private ResponseDTO _response;
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _sendGridKey = _configuration["SendGrid:Key"];
            _response = new ResponseDTO();
        }
        public async Task<ResponseDTO> SendEmailAsync(EmailDTO emailDTO)
        {
            try
            {
                var client = new SendGridClient(_sendGridKey);
                var from = new EmailAddress("iloveyoubaby112709@gmail.com", "Tung Dao");
                var to = new EmailAddress(emailDTO.Email);
                var messageEmail = MailHelper.CreateSingleEmail(from, to, emailDTO.Subject, "", emailDTO.Message);
                var response = await client.SendEmailAsync(messageEmail);
                if(!(response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK))
                {
                    _response.IsSuccess = false;
                    _response.Message = response.StatusCode + response.Body.ToString();
                }
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
