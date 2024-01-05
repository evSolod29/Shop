using System;
using Shop.Infrastructure.MsSql.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Resources;

namespace Shop.Infrastructure.MsSql
{
    public static class DataSeeder
    {
        public static async Task<IApplicationBuilder> Migration(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ShopContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    await context.Database.EnsureDeletedAsync();
                    context.Database.Migrate();
                }
            }
            return builder;
        }
    }
}
