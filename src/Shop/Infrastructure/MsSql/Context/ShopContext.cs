using System;
using Shop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Shop.Infrastructure.MsSql.Context
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }
    }
}
