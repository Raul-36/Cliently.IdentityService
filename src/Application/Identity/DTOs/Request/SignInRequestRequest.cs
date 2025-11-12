using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Identity.DTOs.Request
{
    public class SignInRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
