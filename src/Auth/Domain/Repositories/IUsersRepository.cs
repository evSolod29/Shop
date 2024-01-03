using System;
using Auth.Domain.Models;
using Auth.Domain.Repositories.Base;

namespace Auth.Domain.Repositories
{
    public interface IUsersRepository : IRepository
    {
        Task<User?> FindById(string userId);
        Task<User?> FindByName(string name);
        Task<bool> IsExistId(string id);
        Task<bool> IsExistName(string name);
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task Delete(User user);
        Task SetLockoutUser(string userId, DateTime dateTime);
        Task<bool> CheckPasswordAsync(User user);
        Task AddToRole(User user, string roleName);
        Task RemoveFromRole(User user, string roleName);
        Task<bool> IsInRole(string userId, string role);
        Task<IEnumerable<User>> GetUsers(string? name = null, string? email = null);

    }
}
