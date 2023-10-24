using CleanArchitecture.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Commons
{
    public class LoginResponseDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpire { get; set; }
    }
}
