using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Users.DTOs.Request;
using Application.Users.DTOs.Response;
using Core.Users.Entities.Base;
using MediatR;

namespace Application.Users.Commands
{
    public class UpdateUserCommand : IRequest<Result<UserResponse>>
    {
        public required UpdateUserRequest request { get; set; }
    }
}
