using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Core.Users.Entities.Base;
using MediatR;

namespace Application.Users.Queries
{
    public class GetUserByEmailQuery : IRequest<Result<IUser>>
    {
        public required string Email { get; set; }
    }
}
