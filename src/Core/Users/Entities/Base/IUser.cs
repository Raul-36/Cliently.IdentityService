using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Users.Entities.Base
{
    public interface IUser
    {
        Guid Id { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        string PasswordHash { get; set; }
    }
}
