using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Users.Queries;
using Application.Users.Services.Base;
using Core.Users.Entities.Base;
using MediatR;

namespace Application.Users.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<IUser>>>
    {
        private readonly IUserService _userService;

        public GetAllUsersHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<IEnumerable<IUser>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllUsersAsync();
            return Result<IEnumerable<IUser>>.Success(users);
        }
    }
}
