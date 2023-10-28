using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Entities
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string JwtTokenId { get; set; }
        public string RefreshToken { get; set; }
        public bool IsValidRefreshToken { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
