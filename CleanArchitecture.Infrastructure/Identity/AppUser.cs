using CleanArchitecture.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
