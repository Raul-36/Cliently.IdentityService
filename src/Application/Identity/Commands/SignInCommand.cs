using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Identity.DTOs.Request;
using Application.Identity.DTOs.Response;
using MediatR;

namespace Application.Identity.Commands
{
    public class SignInCommand : IRequest<Result<IdentityResponse>>
    {
        public required SignInRequest request { get; set; }
    }
}