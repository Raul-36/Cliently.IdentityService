using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Users.DTOs.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
    }
}