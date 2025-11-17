using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Core.UserRoles.Entities.Base;

namespace Application.UserRoles.Services.Base
{
    public interface IUserRoleService
    {
        public Task<Result<bool>> AssignRoleToUserAsync(Guid userId, Guid roleId);
        public Task<Result<bool>> UnassignRoleFromUserAsync(Guid userId, Guid roleId);

        public Task<Result<IEnumerable<IUserRole>>> GetByUserId(Guid userId);
        public Task<Result<IEnumerable<IUserRole>>> GetByRoleId(Guid roleId);
    }
}