using Application.Identity.Exceptions;
using Application.Identity.Services.Base;
using Application.Users.Exceptions;
using Core.Users.Entities;
using Infrastructure.Users.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

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
                return false;

            var result = await userManager.CheckPasswordAsync(user, password);

            return result;
        }
    }
}
