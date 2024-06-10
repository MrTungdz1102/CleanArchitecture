namespace CleanArchitecture.Infrastructure.Identity.DTOs
{
    public class UserInfoResponse
    {
        public AppUser User { get; set; }
        public IList<string>? Roles { get; set; }
    }
}
