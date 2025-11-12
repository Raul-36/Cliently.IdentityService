using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Roles.Entities.Base
{
    public interface IRole
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
