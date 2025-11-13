using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.UserRoles.Entities.Base
{
    public interface IUserRole
    {
        Guid UserId { get; set; }
        Guid RoleId { get; set; }
    }
}
