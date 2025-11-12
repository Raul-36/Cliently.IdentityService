using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Roles.Services.Base;
using Application.UserRoles.Services.Base;
using Application.Users.Commands;
using Application.Users.Services.Base;
using Core.Roles.Entities.Base;
using Core.Users.Entities;
using Core.Users.Entities.Base;
using MediatR;

namespace Application.Users.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<IUser>>
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IUserRoleService userRoleService;
        public CreateUserHandler(IUserService userService, IRoleService roleService,IUserRoleService userRoleService)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.userRoleService = userRoleService;
        }
        public async Task<Result<IUser>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.CreateUserAsync(request.request);

            if (request.RoleName == null)
                return Result<IUser>.Success(user);
                
            IRole? role = await roleService.GetRoleByNameAsync(request.RoleName);

            if (role == null)
                return Result<IUser>.Failure($"Role '{request.RoleName}' not found.");

            await userRoleService.AssignRoleToUserAsync(user.Id,  role.Id);
            
            return Result<IUser>.Success(user);
        }
    }
}