using System;
using Auth.Infrastructure.MsSql.Context;
using Auth.Infrastructure.MsSql.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Auth.Domain.Repositories.Base;
using Microsoft.AspNetCore.Builder;

namespace Auth.Infrastructure.MsSql
{
    public static class DI
    {
        public static IServiceCollection InjectInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration["MSSQL:ConnectionString"] ??
                throw new NullReferenceException("Section \"MSSQL:ConnectionString\" not found in configuration file.");
            services.AddDbContext<AuthContext>(x => x.UseSqlServer(connectionString));
            services.AddIdentity<AuthUser, IdentityRole>(
                options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.User.RequireUniqueEmail = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
            .AddEntityFrameworkStores<AuthContext>()
            .AddDefaultTokenProviders(); ;

            services.Scan(scan => scan.FromCallingAssembly()
            .AddClasses(classes =>
                classes.AssignableTo(typeof(IRepository)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            );
            return services;
        }
    }
}
