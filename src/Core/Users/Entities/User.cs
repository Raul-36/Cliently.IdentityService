using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Users.Entities.Base;

namespace Core.Users.Entities
{
    public class User : IUser
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
    }
}