using AutoMapper;
using Application.Users.DTOs.Request;
using Application.Users.DTOs.Response;
using Core.Users.Entities.Base;
using Core.Users.Entities;

namespace Application.Users.Mappings
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<UserResponse, JWTUserRequest>();
            CreateMap<IUser, UserResponse>();
        }
    }
}