using System;
using Auth.Infrastructure.MsSql.Context;
using Auth.Infrastructure.MsSql.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure.MsSql
{
    public static class DI
    {
        public static IServiceCollection InjectInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration["MSSQL:ConnectionString"] ??
                throw new NullReferenceException("Section \"MSSQL:ConnectionString\" not found in configuration file.");
            services.AddDbContext<AuthContext>(x => x.UseSqlServer(connectionString));
            services.AddIdentity<AuthUser, AuthRole>(
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

            return services;
        }
    }
}
