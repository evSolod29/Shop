using Microsoft.AspNetCore.Identity;

namespace Auth.Infrastructure.MsSql.Entities
{
    public class AuthUser : IdentityUser
    {
        public bool IsLocked { get; set; }
    }
}
