using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Core.Users.Entities.Base;
using MediatR;

namespace Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<Result<IEnumerable<IUser>>>
    {
    }
}
