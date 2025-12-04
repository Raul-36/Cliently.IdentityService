using Infrastructure.Roles.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Core.Roles.Entities;
using Presentation.Options;
using Application.Roles.Services.Base;
using Application.Roles.Exceptions;
using Presentation.Consts;

namespace Presentation.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var defoltRolesType = typeof(DefaultRoles);
            var roleNames = defoltRolesType.GetFields()
                .Select(f => f.GetValue(null)?.ToString())
                .Where(rn => rn != null);

            var roleService = serviceProvider.GetRequiredService<IRoleService>();
            
            try
            {
                await roleService.GetRoleByNameAsync(DefaultRoles.Admin);
                return;
            }
            catch(RoleNotFoundException)
            {
                if (roleNames == null) 
                    throw new InvalidOperationException("No roles configured to seed.");

                foreach (var roleName in roleNames)
                {   
                    if (roleName == null) 
                        throw new InvalidOperationException("No roles configured to seed.");
                    await roleService.CreateAsync(roleName);
                }
            }
        }
    }
}
