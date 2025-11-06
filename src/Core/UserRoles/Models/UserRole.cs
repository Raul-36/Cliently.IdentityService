using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.UserRoles.Models
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}