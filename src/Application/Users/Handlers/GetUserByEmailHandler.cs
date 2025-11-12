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
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, Result<IUser>>
    {
        private readonly IUserService userService;

        public GetUserByEmailHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<Result<IUser>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await this.userService.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return Result<IUser>.Failure("User not found");
            }
            return Result<IUser>.Success(user);
        }
    }
}
