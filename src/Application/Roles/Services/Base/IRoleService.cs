using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Roles.Entities.Base;

namespace Application.Roles.Services.Base
{
    public interface IRoleService
    {
        Task<IRole> CreateAsync(string roleName);
        Task<IRole> GetRoleByNameAsync(string name);
        Task<IRole> GetRoleByIdAsync(Guid id);
        Task<IEnumerable<IRole>> GetRoleByNameAsync(IEnumerable<string> names);
        Task<IEnumerable<IRole>> GetRoleByIdAsync(IEnumerable<Guid> ids);
        Task<IEnumerable<IRole>> GetAllAsync();
        Task DeleteAsync(Guid roleId);
    }
}