using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Roles.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}