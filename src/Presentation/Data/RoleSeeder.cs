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

namespace Presentation.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<IOptions<RolesOptions>>();
            var roleService = serviceProvider.GetRequiredService<IRoleService>();

            var roleNames = options.Value.RoleNames;
            try
            {
                await roleService.GetRoleByNameAsync("Admin");
                return;
            }
            catch(RoleNotFoundException)
            {
                if (roleNames == null) 
                    throw new InvalidOperationException("No roles configured to seed.");

                foreach (var roleName in roleNames)
                {   
                    await roleService.CreateAsync(roleName);
                }
            }
        }
    }
}
