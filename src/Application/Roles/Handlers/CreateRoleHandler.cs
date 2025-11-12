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
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, Result<bool>>
    {
        private readonly IRoleService roleService;

        public CreateRoleHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public async Task<Result<bool>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await this.roleService.CreateAsync(request.Name);
            if (!result)
            {
                return Result<bool>.Failure("Role could not be created.");
            }
            return Result<bool>.Success(result);
        }
    }
}
