using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Roles.Queries;
using Application.Roles.Services.Base;
using Core.Roles.Entities.Base;
using MediatR;

namespace Application.Roles.Handlers
{
    public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, Result<IRole>>
    {
        private readonly IRoleService _roleService;

        public GetRoleByIdHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Result<IRole>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleService.GetRoleByIdAsync(request.Id);
            if (role == null)
            {
                return Result<IRole>.Failure("Role not found");
            }
            return Result<IRole>.Success(role);
        }
    }
}
