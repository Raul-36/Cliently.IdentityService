using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Users.DTOs.Request;

namespace Application.Tokens.DTOs.Request
{
    public class GenerateJWTTokenRequest
    {
        public required JWTUserRequest User { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}