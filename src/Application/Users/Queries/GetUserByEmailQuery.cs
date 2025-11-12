using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Users.DTOs.Response;
using Core.Users.Entities.Base;
using MediatR;

namespace Application.Users.Queries
{
    public class GetUserByEmailQuery : IRequest<Result<UserResponse>>
    {
        public required string Email { get; set; }
    }
}
