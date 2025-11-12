using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Users.DTOs.Request
{
    public class UpdateUserRequest
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
    }
}
