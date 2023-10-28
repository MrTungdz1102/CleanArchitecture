using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailAPIController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailAPIController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("SendEmail")]
        public async Task<ActionResult<ResponseDTO>> SendEmail(EmailDTO emailDTO)
        {
            return Ok(await _emailSender.SendEmailAsync(emailDTO));
        }
    }
}
