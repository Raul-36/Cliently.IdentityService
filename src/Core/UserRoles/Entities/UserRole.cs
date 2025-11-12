using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.UserRoles.Entities.Base;

namespace Core.UserRoles.Entities
{
    public class UserRole : IUserRole
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}