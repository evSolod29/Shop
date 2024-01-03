using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Auth.Infrastructure.MsSql.Context
{
    public class AuthContextFactory : IDesignTimeDbContextFactory<AuthContext>
    {
        public AuthContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=localhost;";
            if (args != null && args.Any() && !string.IsNullOrEmpty(args[0]))
                connectionString = args[0];
            var optionsBuilder = new DbContextOptionsBuilder<AuthContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AuthContext(optionsBuilder.Options);
        }
    }
}
