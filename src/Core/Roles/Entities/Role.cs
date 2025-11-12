using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Roles.Entities.Base;

namespace Core.Roles.Entities
{
    public class Role : IRole
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}