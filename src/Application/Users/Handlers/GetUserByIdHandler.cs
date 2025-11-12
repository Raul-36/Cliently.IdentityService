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
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Result<IUser>>
    {
        private readonly IUserService _userService;

        public GetUserByIdHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<IUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(request.Id);
            if (user == null)
            {
                return Result<IUser>.Failure("User not found");
            }
            return Result<IUser>.Success(user);
        }
    }
}
