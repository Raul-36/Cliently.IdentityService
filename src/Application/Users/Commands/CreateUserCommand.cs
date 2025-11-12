using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Users.DTOs.Request;
using Application.Users.DTOs.Response;
using Core.Users.Entities;
using Core.Users.Entities.Base;
using MediatR;

namespace Application.Users.Commands
{
    public class CreateUserCommand : IRequest<Result<UserResponse>>
    {
        public required CreateUserRequest request { get; set; }
    }
}