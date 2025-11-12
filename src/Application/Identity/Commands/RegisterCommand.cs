using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Identity.DTOs.Response;
using Application.Users.Commands;
using Application.Users.DTOs.Request;
using MediatR;

namespace Application.Identity.Commands
{
    public class RegisterCommand : IRequest<Result<IdentityResponse>>
    {
        public required CreateUserRequest createUser { get; set; }
        public required string RoleName { get; set; }
    }
}