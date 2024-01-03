using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Interfaces.Base;

namespace Shop.Application
{
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

            return services;
        }
    }
}
