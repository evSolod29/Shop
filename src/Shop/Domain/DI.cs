using System.ComponentModel;
using System.Reflection;
// using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Domain
{
    public static class DI
    {
        // public static IServiceCollection DomainLayerInit(this IServiceCollection services)
        // {
        //     ValidatorOptions.Global.DisplayNameResolver = (type, member, expression) =>
        //     {
        //         if (member != null)
        //         {
        //             return GetPropertyDisplayName(member);
        //         }
        //         return Resources.Strings.UNKNOWN;
        //     };

        //     return services;
        // }

        // public static string GetPropertyDisplayName(MemberInfo memberInfo)
        // {
        //     var attr = memberInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false)
        //         .SingleOrDefault();

        //     if (attr is not DisplayNameAttribute nameAttr)
        //         return memberInfo.Name;

        //     return nameAttr.DisplayName;
        // }
    }
}
