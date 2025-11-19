using Application.Common.Exceptions;
using Application.Roles.Exceptions;
using Application.UserRoles.Services.Base;
using Application.Users.Exceptions;
using Core.UserRoles.Entities.Base;
using Infrastructure.Roles.Entities;
using Infrastructure.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Core.UserRoles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.UserRoles.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UserRoleService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<bool> AssignRoleToUserAsync(Guid userId, Guid roleId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new UserNotFoundException(userId);

            var role = await roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                throw new RoleNotFoundException(roleId);

            if (role.Name == null)
                throw new InvalidOperationException("Role name is null.");

            var result = await userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new BadRequestException(errors);
            }
            return true;
        }

        public async Task<IEnumerable<IUserRole>> GetByRoleId(Guid roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                throw new RoleNotFoundException(roleId);
            
            if (role.Name == null)
                throw new InvalidOperationException("Role name is null.");

            var usersInRole = await userManager.GetUsersInRoleAsync(role.Name);
            var userRoles = new List<IUserRole>();
            foreach (var user in usersInRole)
            {
                userRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
            }
            return userRoles;
        }

        public async Task<IEnumerable<IUserRole>> GetByUserId(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            var roles = await userManager.GetRolesAsync(user);
            var userRoles = new List<IUserRole>();
            foreach (var roleName in roles)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    userRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
                }
            }
            return userRoles;
        }

        public async Task<bool> UnassignRoleFromUserAsync(Guid userId, Guid roleId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new UserNotFoundException(userId);

            var role = await roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                throw new RoleNotFoundException(roleId);

            if (role.Name == null)
                throw new InvalidOperationException("Role name is null.");

            var result = await userManager.RemoveFromRoleAsync(user, role.Name);
            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new BadRequestException(errors);
            }
            return true;
        }
    }
}
