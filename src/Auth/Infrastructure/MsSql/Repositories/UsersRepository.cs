using Auth.Domain.Exceptions;
using Auth.Domain.Models;
using Auth.Domain.Repositories;
using Auth.Infrastructure.MsSql.Entities;
using Auth.Infrastructure.MsSql.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.MsSql.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<AuthUser> userManager;

        public UsersRepository(UserManager<AuthUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task AddToRole(User user, string roleName)
        {
            AuthUser? authUser = await userManager.FindByIdAsync(user.Id);
            var result = await userManager.AddToRoleAsync(authUser, roleName);
            if (!result.Succeeded)
                throw new RepositoryException(result.Errors.First().Description);
        }

        public async Task<bool> CheckPasswordAsync(User user)
        {
            AuthUser? authUser = await userManager.FindByIdAsync(user.Id);
            return await userManager.CheckPasswordAsync(authUser, user.Password);
        }

        public async Task<User> Create(User user)
        {
            AuthUser authUser = user.ToAuthUser();
            authUser.Id = Guid.NewGuid().ToString();
            await userManager.CreateAsync(authUser, user.Password);
            return authUser.ToUser();
        }

        public async Task Delete(User user)
        {
            AuthUser? authUser = await userManager.FindByIdAsync(user.Id);
            await userManager.DeleteAsync(authUser);
        }

        public async Task<User?> FindById(string userId)
        {
            return (await userManager.FindByIdAsync(userId)).ToUser();
        }

        public async Task<User?> FindByName(string name)
        {
            return (await userManager.FindByNameAsync(name)).ToUser();
        }

        public async Task<bool> IsInRole(string userId, string role)
        {
            AuthUser authUser = await userManager.FindByIdAsync(userId);
            return await userManager.IsInRoleAsync(authUser, role);
        }


        public async Task<bool> IsExistId(string id)
        {
            return await userManager.Users.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsExistName(string name)
        {
            return await userManager.Users
                .AnyAsync(x => x.UserName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task RemoveFromRole(User user, string roleName)
        {
            AuthUser? authUser = await userManager.FindByIdAsync(user.Id);
            var result = await userManager.RemoveFromRoleAsync(authUser, roleName);
            if (!result.Succeeded)
                throw new RepositoryException(result.Errors.First().Description);
        }

        public async Task SetLockoutUser(string userId, DateTime dateTime)
        {
            AuthUser? authUser = await userManager.FindByIdAsync(userId);
            var result = await userManager.SetLockoutEndDateAsync(authUser, dateTime);
            if (!result.Succeeded)
                throw new RepositoryException(result.Errors.First().Description);
        }

        public async Task<User> Update(User user)
        {
            AuthUser? authUser = await userManager.FindByIdAsync(user.Id);
            authUser = user.ToAuthUser(authUser);
            await userManager.UpdateAsync(authUser);
            return authUser.ToUser();
        }

        public async Task<IEnumerable<User>> GetUsers(string? name = null, string? email = null)
        {
            return (await userManager.Users.AsNoTracking()
                .Where(x =>
                    (string.IsNullOrEmpty(name) || x.UserName.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrEmpty(email) || x.Email.Contains(email, StringComparison.OrdinalIgnoreCase)))
                .ToListAsync())
                .ToIEnumerableUser();
        }

        public async Task<IEnumerable<string>> GetUserRoles(User user)
        {
            AuthUser authUser = await userManager.FindByIdAsync(user.Id);
            return await userManager.GetRolesAsync(authUser);
        }
    }
}
