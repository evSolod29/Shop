using System;
using Auth.Application.Exceptions;
using Auth.Application.Interfaces.Base;
using Auth.Domain.Models;
using Auth.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using Shared.DTO;
using Shared.DTO.DTO.Users;
using Shared.Resources;

namespace Auth.Application.Services
{
    public class AuthorizationServise : IAuthorizationServise
    {
        private readonly IMapper mapper;
        private readonly IUsersRepository users;
        private readonly IValidator<CreateUser> validator;


        public AuthorizationServise(IMapper mapper, IUsersRepository users, IValidator<CreateUser> validator)
        {
            this.mapper = mapper;
            this.users = users;
            this.validator = validator;

        }


        public async Task<AuthDetails> Authorization(string userName, string password)
        {
            User user = await users.FindByName(userName)
                ?? throw new IncorrectParametersException(Strings.CredentialsError);
            user.Password = password;
            if (!await users.CheckPasswordAsync(user))
                throw new IncorrectParametersException(Strings.CredentialsError);
            if (user.IsLocked)
                throw new IncorrectParametersException(Strings.UserIsBlocked);
        }

        public async Task<string> Registrtion(CreateUser createUser)
        {
            try
            {
                if (await users.IsExistName(createUser.Name))
                    throw new NotFoundException(Strings.UserAlreadyExist);
                await validator.ValidateAndThrowAsync(createUser);
                User user = mapper.Map<User>(createUser);
                user = await users.Create(user);
                await users.AddToRole(user, Roles.DefaultRole);
                return user.Name;
            }
            catch (ValidationException ex)
            {
                throw new IncorrectParametersException(ex.Errors.First().ErrorMessage);
            }
        }

    }
}
