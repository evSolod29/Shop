using Auth.Infrastructure.MsSql.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.MsSql.Context
{
    public class AuthContext : IdentityDbContext<AuthUser>
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }
    }
}
