using Application.Common;
using Application.UserRoles.Services.Base;
using AutoMapper;
using Core.UserRoles.Entities.Base;
using Infrastructure.Roles.Entities;
using Infrastructure.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Result<bool>> AssignRoleToUserAsync(Guid userId, Guid roleId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            var role = await roleManager.FindByIdAsync(roleId.ToString());

            if (user == null)
                return Result<bool>.Failure("User not found.");
        
            if (role == null)
                return Result<bool>.Failure("Role not found.");

            if (role.Name == null)
                return Result<bool>.Failure("Role name is null.");


            var result = await userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Result<bool>.Failure(errors);
            }
            return Result<bool>.Success(true);
        }

        public async Task<Result<IEnumerable<IUserRole>>> GetByRoleId(Guid roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                return Result<IEnumerable<IUserRole>>.Failure("Role not found.");
            
            if (role.Name == null)
                return Result<IEnumerable<IUserRole>>.Failure("Role name is null.");

            var usersInRole = await userManager.GetUsersInRoleAsync(role.Name);
            var userRoles = new List<IUserRole>();
            foreach (var user in usersInRole)
            {
                userRoles.Add(new Core.UserRoles.Entities.UserRole { UserId = user.Id, RoleId = role.Id });
            }
            return Result<IEnumerable<IUserRole>>.Success(userRoles);
        }

        public async Task<Result<IEnumerable<IUserRole>>> GetByUserId(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return Result<IEnumerable<IUserRole>>.Failure("User not found.");
            }

            var roles = await userManager.GetRolesAsync(user);
            var userRoles = new List<IUserRole>();
            foreach (var roleName in roles)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    userRoles.Add(new Core.UserRoles.Entities.UserRole { UserId = user.Id, RoleId = role.Id });
                }
            }
            return Result<IEnumerable<IUserRole>>.Success(userRoles);
        }

        public async Task<Result<bool>> UnassignRoleFromUserAsync(Guid userId, Guid roleId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            var role = await roleManager.FindByIdAsync(roleId.ToString());

            if (user == null)
                return Result<bool>.Failure("User not found.");
            if (role == null)
                return Result<bool>.Failure("Role not found.");

            if (role.Name == null)
                return Result<bool>.Failure("Role name is null.");
            var result = await userManager.RemoveFromRoleAsync(user, role.Name);

            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Result<bool>.Failure(errors);
            }
            return Result<bool>.Success(true);
        }
    }
}
