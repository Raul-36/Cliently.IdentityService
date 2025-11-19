using Application.Identity.DTOs.Request;
using Application.Identity.DTOs.Response;
using MediatR;

namespace Application.Identity.Commands
{
    public class SignInCommand : IRequest<IdentityResponse>
    {
        public required SignInRequest request { get; set; }
    }
}