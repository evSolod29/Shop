using System;
using Auth.Application.Exceptions;
using Auth.Application.Interfaces;
using Auth.Domain.Models;
using Auth.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using Shared.DTO.DTO.Users;
using Shared.Resources;
using Role = Shared.DTO.Role;

namespace Auth.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUsersRepository users;
        private readonly IValidator<CreateUser> validator;


        public UserService(IMapper mapper, IUsersRepository users, IValidator<CreateUser> validator)
        {
            this.mapper = mapper;
            this.users = users;
            this.validator = validator;

        }
        public async Task AddRole(string userId, Role role)
        {
            User user = await users.FindById(userId) ??
                throw new NotFoundException(Strings.UserNotFound);
            string strRole = role switch
            {
                Role.User => Roles.User,
                Role.SuperUser => Roles.SuperUser,
                Role.Admin => Roles.Admin,
                _ => throw new IncorrectParametersException(Strings.RoleNotFound)
            };

            if (await users.IsInRole(userId, strRole))
                throw new IncorrectParametersException(Strings.UserAlreadyHaveRole);
            await users.AddToRole(user, strRole);
        }

        public async Task Delete(string id)
        {
            User user = await users.FindById(id) ??
                throw new NotFoundException(Strings.UserNotFound);
            await users.Delete(user);
            return;
        }

        public async Task<IEnumerable<ViewUser>> Get(string? name = null, string? email = null)
        {
            return mapper.Map<IEnumerable<ViewUser>>(await users.GetUsers(name, email));
        }

        public async Task<ViewUser> GetById(string id)
        {
            User user = await users.FindById(id) ??
                throw new NotFoundException(Strings.UserNotFound);
            return mapper.Map<ViewUser>(user);
        }

        public async Task<ViewUser> GetByName(string name)
        {
            User user = await users.FindByName(name) ??
                throw new NotFoundException(Strings.UserNotFound);
            return mapper.Map<ViewUser>(user);
        }

        public async Task<IEnumerable<string>> GetUserRoles(string userId)
        {
            User user = await users.FindById(userId) ??
                throw new NotFoundException(Strings.UserNotFound);
            return await users.GetUserRoles(user);
        }

        public async Task LockUser(string id)
        {
            if (!await users.IsExistId(id))
                throw new NotFoundException(Strings.UserNotFound);
            await users.SetLockoutUser(id, DateTime.MaxValue);
        }

        public async Task RemoveRole(string userId, Role role)
        {
            User user = await users.FindById(userId) ??
                throw new NotFoundException(Strings.UserNotFound);
            string strRole = role switch
            {
                Role.User => Roles.User,
                Role.SuperUser => Roles.SuperUser,
                Role.Admin => Roles.Admin,
                _ => throw new IncorrectParametersException(Strings.RoleNotFound)
            };

            if (!await users.IsInRole(userId, strRole))
                throw new IncorrectParametersException(Strings.UserAlreadyHaveNotRole);
            await users.AddToRole(user, strRole);
        }

        public async Task UnlockUser(string id)
        {
            if (!await users.IsExistId(id))
                throw new NotFoundException(Strings.UserNotFound);
            await users.SetLockoutUser(id, DateTime.MinValue);
        }

        public async Task<string> Update(string id, CreateUser createUser)
        {
            User user = await users.FindById(id) ??
                throw new NotFoundException(Strings.UserNotFound);
            try
            {
                if (!user.Name.Equals(createUser.Name, StringComparison.OrdinalIgnoreCase))
                    if (await users.IsExistName(createUser.Name))
                        throw new NotFoundException(Strings.UserAlreadyExist);
                await validator.ValidateAndThrowAsync(createUser);
                user = mapper.Map(createUser, user);
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
