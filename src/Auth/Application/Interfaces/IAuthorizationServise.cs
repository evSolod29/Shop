using System;
using Shared.DTO;
using Shared.DTO.DTO.Users;

namespace Auth.Application.Interfaces.Base
{
    public interface IAuthorizationService : IService
    {
        Task<AuthDetails> Authorization(string userName, string password);
        Task<string> Registrtion(CreateUser createUser);
    }
}
