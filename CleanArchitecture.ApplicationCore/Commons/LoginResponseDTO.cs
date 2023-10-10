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
        public AppUser appUser { get; set; }
        public string Token { get; set; }
    }
}
