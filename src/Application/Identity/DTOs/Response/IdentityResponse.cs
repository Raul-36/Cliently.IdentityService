using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Users.Commands;
using Application.Users.DTOs.Response;

namespace Application.Identity.DTOs.Response
{
    public class IdentityResponse
    {
        public required UserResponse User{ get; set; }
        public required string Token { get; set; }
    }
}