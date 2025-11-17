using Infrastructure.Roles.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Core.Roles.Entities;
using Presentation.Options;

namespace Presentation.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var options = serviceProvider.GetRequiredService<IOptions<RolesOptions>>();

            var roleNames = options.Value.RolesNames;

            if (roleNames == null) return;

            foreach (var roleName in roleNames)
            {   
                await roleManager.CreateAsync(new ApplicationRole {Name = roleName});
            }
        }
    }
}
