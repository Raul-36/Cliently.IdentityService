using Core.Users.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Users.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IUser
    {
        
    }
}
