using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.WebUI.Models
{
    public class AppUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        [NotMapped]
        [ValidateNever]
        public string[] Roles { get; set; }

        [NotMapped]
        [ValidateNever]
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
