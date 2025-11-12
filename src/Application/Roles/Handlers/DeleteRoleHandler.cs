using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Roles.Commands;
using Application.Roles.Services.Base;
using MediatR;

namespace Application.Roles.Handlers
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, Result<bool>>
    {
        private readonly IRoleService roleService;

        public DeleteRoleHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public async Task<Result<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await this.roleService.DeleteAsync(request.RoleId);
            if (!result)
            {
                return Result<bool>.Failure("Role could not be deleted.");
            }
            return Result<bool>.Success(result);
        }
    }
}
