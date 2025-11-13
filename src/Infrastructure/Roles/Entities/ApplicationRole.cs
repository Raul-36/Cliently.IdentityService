using Core.Roles.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Roles.Entities
{
    public class ApplicationRole : IdentityRole<Guid>, IRole
    {
        
    }
}
