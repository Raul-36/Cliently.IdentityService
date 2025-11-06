using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Users.DTOs.Request
{
    public class CreateUserRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}