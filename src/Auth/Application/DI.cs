using System.Reflection;
using Auth.Application.Interfaces.Base;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Utils.Interfaces;
using Shared.Utils.Services;

namespace Auth.Application;
public static class DI
{
    public static IServiceCollection InjectApplicationDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.Scan(scan => scan.FromCallingAssembly()
            .AddClasses(classes =>
                classes.AssignableTo(typeof(IService)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
        );

        services.AddScoped<ITokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
