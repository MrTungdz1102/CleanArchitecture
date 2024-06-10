using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }

        [NotMapped]
        public IList<string>? Roles { get; set; }
    }
}
