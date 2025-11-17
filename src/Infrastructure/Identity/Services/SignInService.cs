using Application.Identity.Services.Base;
using Infrastructure.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Services
{
    public class SignInService : ISignInService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public SignInService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> PasswordSignInAsync(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            var result = await userManager.CheckPasswordAsync(user, password);
            return result;
        }
    }
}
