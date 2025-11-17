using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Core.Roles.Entities;
using Core.Roles.Entities.Base;

namespace Application.Roles.Services.Base
{
    public interface IRoleService
    {
        public Task<Result<IRole>> CreateAsync(string roleName);
        public Task<Result<IRole>> GetRoleByNameAsync(string name);
        public Task<Result<IRole>> GetRoleByIdAsync(Guid id);
        public Task<Result<IEnumerable<IRole>>> GetRoleByNameAsync(IEnumerable<string> names);
        public Task<Result<IEnumerable<IRole>>> GetRoleByIdAsync(IEnumerable<Guid> ids);
        public Task<Result<IEnumerable<IRole>>> GetAllAsync();
    }
}