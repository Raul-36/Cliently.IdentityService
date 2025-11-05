using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.SignIns.Services.Base
{
    public interface ISignInService
    {
        public Task<bool> PasswordSignInAsync(string email, string password);
    }
}