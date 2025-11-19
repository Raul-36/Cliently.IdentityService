using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.UserRoles.Entities.Base;

namespace Application.UserRoles.Services.Base
{
    public interface IUserRoleService
    {
        Task<bool> AssignRoleToUserAsync(Guid userId, Guid roleId);
        Task<bool> UnassignRoleFromUserAsync(Guid userId, Guid roleId);
        Task<IEnumerable<IUserRole>> GetByUserId(Guid userId);
        Task<IEnumerable<IUserRole>> GetByRoleId(Guid roleId);
    }
}