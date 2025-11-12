using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UserRoles.Services.Base
{
    public interface IUserRoleService
    {
        public Task<bool> AssignRoleToUserAsync(Guid userId, Guid roleId);
        public Task<bool> UnassignRoleFromUserAsync(Guid userId, Guid roleId);
        public Task<bool> RemoveRoleFromUserAsync(Guid userId, Guid roleId);
    }
}