using CleanArchitecture.ApplicationCore.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Interfaces.Commons
{
    public interface IEmailSender
    {
        Task<ResponseDTO> SendEmailAsync(EmailDTO emailDTO);
    }
}
