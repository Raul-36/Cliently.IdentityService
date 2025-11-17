using Application.Identity.Services.Base;
using Infrastructure.Users.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Services
{
    public class SignInService : ISignInService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public SignInService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<bool> PasswordSignInAsync(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
            return result.Succeeded;
        }
    }
}
