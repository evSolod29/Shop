using System;
using Auth.Domain.Models;
using Auth.Infrastructure.MsSql.Context;
using Auth.Infrastructure.MsSql.Entities;
using Auth.Infrastructure.MsSql.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Resources;

namespace Auth.Infrastructure.MsSql
{
    public static class DataSeeder
    {
        public static async Task<IApplicationBuilder> Seed(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AuthContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    await context.Database.EnsureDeletedAsync();
                    context.Database.Migrate();

                    await AddRoles(scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>());
                    await AddUsers(scope.ServiceProvider.GetRequiredService<UserManager<AuthUser>>());
                }
            }
            return builder;
        }

        private static async Task AddRoles(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = Roles.User
            });
            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = Roles.SuperUser
            });
            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = Roles.Admin
            });
        }


        private static async Task AddUsers(UserManager<AuthUser> users)
        {
            var user = new AuthUser()
            {
                UserName = Roles.User,
                Email = "awa@gmail.com",
            };
            await users.CreateAsync(user, "123456");
            await users.AddToRoleAsync(user, Roles.User);

            var superUser = new AuthUser()
            {
                UserName = Roles.SuperUser,
                Email = "awa@gmail.com",
            };
            await users.CreateAsync(superUser, "123456");
            await users.AddToRoleAsync(superUser, Roles.SuperUser);

            var admin = new AuthUser()
            {
                UserName = Roles.Admin,
                Email = "awa@gmail.com"
            };
            await users.CreateAsync(admin, "123456");
            await users.AddToRoleAsync(admin, Roles.Admin);
        }
    }
}
