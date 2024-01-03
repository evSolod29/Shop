using System;
using Auth.Domain.Models;
using Auth.Infrastructure.MsSql.Entities;

namespace Auth.Infrastructure.MsSql.Extensions
{
    public static class MappingExtensions
    {
        public static AuthUser ToAuthUser(this User user) => new()
        {
            Id = user.Id,
            UserName = user.Name,
            Email = user.Email,

        };

        public static AuthUser ToAuthUser(this User source, AuthUser dest)
        {
            dest.UserName = source.Name;
            dest.Email = source.Email;
            return dest;
        }

        public static User ToUser(this AuthUser user) => new()
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email,
            IsLocked = user.LockoutEnd > DateTime.Now
        };
        public static IEnumerable<User> ToIEnumerableUser(this IEnumerable<AuthUser> authUsers)
            => authUsers.Select(x => x.ToUser());
    }
}
