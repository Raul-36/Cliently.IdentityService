using Application.Common;
using Application.Roles.Services.Base;
using AutoMapper;
using Core.Roles.Entities.Base;
using Infrastructure.Roles.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Roles.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IMapper mapper;

        public RoleService(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public async Task<Result<IRole>> CreateAsync(string roleName)
        {
            var role = new ApplicationRole { Name = roleName };
            var createResult = await roleManager.CreateAsync(role);
            if (createResult.Succeeded == false)
            {
                var errors = createResult.Errors.Select(e => e.Description);
                return Result<IRole>.Failure(errors);
            }
            var created = await roleManager.FindByNameAsync(roleName);
            if (created == null)
            {
                return Result<IRole>.Failure("Role creation failed.");
            }
            return Result<IRole>.Success(created);
        }

        public async Task<Result<bool>> DeleteAsync(Guid roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                return Result<bool>.Failure("Role not found.");
            }
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Result<bool>.Failure(errors);
            }
            return Result<bool>.Success(true);
        }

        public async Task<Result<IEnumerable<IRole>>> GetAllAsync()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return Result<IEnumerable<IRole>>.Success(roles);
        }

        public async Task<Result<IEnumerable<IRole>>> GetRoleByIdAsync(IEnumerable<Guid> ids)
        {
            var roles = await roleManager.Roles
                .Where(r => ids.Contains(r.Id))
                .ToListAsync();

            if (roles.Any() == false)
            {
                return Result<IEnumerable<IRole>>.Failure("Roles not found");
            }
            
            return Result<IEnumerable<IRole>>.Success(roles);
        }

        public async Task<Result<IRole>> GetRoleByIdAsync(Guid id)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return Result<IRole>.Failure("Role not found");
            }
            return Result<IRole>.Success(role);
        }

        public async Task<Result<IEnumerable<IRole>>> GetRoleByNameAsync(IEnumerable<string> names)
        {
            var roles = await roleManager.Roles
                .Where(r => names.Contains(r.Name))
                .ToListAsync();
            if (roles.Any() == false)
            {
                return Result<IEnumerable<IRole>>.Failure("Roles not found");
            }
            return Result<IEnumerable<IRole>>.Success(roles);
        }

        public async Task<Result<IRole>> GetRoleByNameAsync(string name)
        {
            var role = await roleManager.FindByNameAsync(name);
            if (role == null)
            {
                return Result<IRole>.Failure("Role not found");
            }
            return Result<IRole>.Success(role);
        }
    }
}
