using System;
using Microsoft.EntityFrameworkCore;

namespace Shop.Infrastructure.MsSql.Context
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }
    }
}
