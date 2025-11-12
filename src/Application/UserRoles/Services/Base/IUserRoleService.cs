using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.UserRoles.Entities.Base;

namespace Application.UserRoles.Services.Base
{
    public interface IUserRoleService
    {
        public Task<bool> AssignRoleToUserAsync(Guid userId, Guid roleId);
        public Task<bool> UnassignRoleFromUserAsync(Guid userId, Guid roleId);

        public Task<IEnumerable<IUserRole>> GetByUserId(Guid userId);
        public Task<IEnumerable<IUserRole>> GetByRoleId(Guid roleId);
    }
}