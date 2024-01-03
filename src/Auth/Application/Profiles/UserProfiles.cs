using Auth.Domain.Models;
using AutoMapper;
using Shared.DTO.DTO.Users;

namespace Auth.Application.Profiles;

public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<CreateUser, User>();
        CreateMap<User, ViewUser>();
    }
}
