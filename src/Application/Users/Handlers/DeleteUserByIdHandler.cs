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
            var result = await userService.DeleteUserByIdAsync(request.Id);
            return Result<bool>.Success(result);
        }
    }
}