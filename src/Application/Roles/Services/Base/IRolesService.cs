using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Roles.Services.Base
{
    public interface IRolesService
    {
        public Task<bool> CreateAsync(string roleName);
    }
}