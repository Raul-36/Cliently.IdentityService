using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Users.Commands;
using Application.Users.Services.Base;
using Core.Users.Entities.Base;
using MediatR;

namespace Application.Users.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<IUser>>
    {
        private readonly IUserService userService;

        public UpdateUserHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<Result<IUser>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await this.userService.GetUserByIdAsync(request.request.Id);
            if (user == null)
            {
                return Result<IUser>.Failure("User not found");
            }

            user.Email = request.request.Email;

            var updatedUser = await this.userService.UpdateUserAsync(user);

            return Result<IUser>.Success(updatedUser);
        }
    }
}
