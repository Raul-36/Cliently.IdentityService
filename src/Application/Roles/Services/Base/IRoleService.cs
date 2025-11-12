using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Roles.Entities;
using Core.Roles.Entities.Base;

namespace Application.Roles.Services.Base
{
    public interface IRoleService
    {
        public Task<bool> CreateAsync(string roleName);
        public Task<IRole?> GetRoleByNameAsync(string name);
        public Task<IRole?> GetRoleByIdAsync(Guid id);
        public Task<IEnumerable<IRole>> GetRoleByNameAsync(IEnumerable<string> names);
        public Task<IEnumerable<IRole>> GetRoleByIdAsync(IEnumerable<Guid> ids);
        public Task<bool> DeleteAsync(Guid roleId);
    }
}