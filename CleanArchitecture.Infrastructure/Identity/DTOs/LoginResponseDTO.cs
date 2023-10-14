using CleanArchitecture.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity.DTOs
{
    public class LoginResponseDTO
    {
        public AppUser AppUser { get; set; }
        public string Token { get; set; }
    }
}
