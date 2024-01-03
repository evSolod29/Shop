using System;
using Auth.Application.Interfaces.Base;
using Shared.DTO;
using Shared.DTO.DTO.Users;

namespace Auth.Application.Interfaces
{
    public interface IUserService : IService
    {
        Task<IEnumerable<ViewUser>> Get(string? name = null, string? email = null);
        Task<ViewUser> GetById(string id);
        Task<ViewUser> GetByName(string name);
        Task<string> Update(string id, CreateUser createUser);
        Task Delete(string id);
        Task LockUser(string id);
        Task UnlockUser(string id);
        Task AddRole(string userId, Role role);
        Task RemoveRole(string userId, Role role);
    }
}
