using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Users.Commands;
using Application.Users.Services.Base;
using MediatR;

namespace Application.Users.Handlers
{
    public class DeleteUserByIdHandler : IRequestHandler<DeleteUserByIdCommand, Result<bool>>
    {
        private readonly IUserService userService;
        public DeleteUserByIdHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<Result<bool>> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            var deleteUserResult = await userService.DeleteUserByIdAsync(request.Id);
            if (deleteUserResult.IsSuccess == false)
            {
                return Result<bool>.Failure(deleteUserResult.Errors
                ?? new List<string>() { "Unknown error at deleting user" });
            }

            var result = deleteUserResult.Value;
            return Result<bool>.Success(result);
        }
    }
}