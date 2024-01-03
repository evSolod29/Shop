using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Shop.Infrastructure.MsSql.Context
{
    public class ShopContextFactory : IDesignTimeDbContextFactory<ShopContext>
    {
        public ShopContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=localhost;";
            if (args != null && args.Any() && !string.IsNullOrEmpty(args[0]))
                connectionString = args[0];
            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ShopContext(optionsBuilder.Options);
        }

    }
}
