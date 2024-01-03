using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Repositories.Base;
using Shop.Infrastructure.MsSql.Context;

namespace Shop.Infrastructure.MsSql;

public static class DI
{
    public static IServiceCollection InjectInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration["MSSQL:ConnectionString"] ??
            throw new NullReferenceException("Section \"MSSQL:ConnectionString\" not found in configuration file.");

        services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));

        services.AddScoped<DbContext, ShopContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.Scan(scan => scan.FromCallingAssembly()
            .AddClasses(classes =>
                classes.AssignableTo(typeof(IRepository<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
        );

        return services;
    }
}
