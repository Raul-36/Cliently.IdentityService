using Application.Common.Exceptions;
using Application.Roles.Exceptions;
using Application.Roles.Services.Base;
using AutoMapper;
using Core.Roles.Entities;
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

        public async Task<IRole> CreateAsync(string roleName)
        {
            var role = new ApplicationRole { Name = roleName, NormalizedName = roleName.ToUpper() };
            var createResult = await roleManager.CreateAsync(role);
            if (createResult.Succeeded == false)
            {
                var errors = createResult.Errors.Select(e => e.Description);
                throw new BadRequestException(errors);
            }
            var created = await roleManager.FindByNameAsync(roleName);
            if (created == null)
            {
                throw new RoleNotFoundException(roleName);
            }
            return created;
        }

        public async Task DeleteAsync(Guid roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                throw new RoleNotFoundException(roleId);
            }
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new BadRequestException(errors);
            }
        }

        public async Task<IEnumerable<IRole>> GetAllAsync()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<IEnumerable<IRole>> GetRoleByIdAsync(IEnumerable<Guid> ids)
        {
            var roles = await roleManager.Roles
                .Where(r => ids.Contains(r.Id))
                .ToListAsync();

            if (roles.Count != ids.Count())
            {
                var notFoundIds = ids.Except(roles.Select(r => r.Id));
                throw new RoleNotFoundException($"Roles with the following IDs were not found: {string.Join(", ", notFoundIds)}");
            }
            
            return roles;
        }

        public async Task<IRole> GetRoleByIdAsync(Guid id)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                throw new RoleNotFoundException(id);
            }
            return role;
        }

        public async Task<IEnumerable<IRole>> GetRoleByNameAsync(IEnumerable<string> names)
        {
            var roles = await roleManager.Roles
                .Where(r => names.Contains(r.Name))
                .ToListAsync();
            if (roles.Count != names.Count())
            {
                var notFoundMames = names.Except(roles.Select(r => r.Name));
                throw new RoleNotFoundException($"Roles with the following names were not found: {string.Join(", ", notFoundMames)}");
            }
            return roles;
        }

        public async Task<IRole> GetRoleByNameAsync(string name)
        {
            var role = await roleManager.FindByNameAsync(name);
            if (role == null)
            {
                throw new RoleNotFoundException(name);
            }
            return role;
        }
    }
}
