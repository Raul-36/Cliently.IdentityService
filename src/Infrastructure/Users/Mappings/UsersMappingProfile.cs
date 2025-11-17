using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Application.Users.DTOs.Request;
using AutoMapper;
using Core.Users.Entities.Base;
using Infrastructure.Users.Entities;

namespace Infrastructure.Users.Mappings
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<CreateUserRequest, ApplicationUser>();
            CreateMap<IUser, ApplicationUser>();
        }
    }
}