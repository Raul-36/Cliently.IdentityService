using Application.Identity.DTOs.Response;
using Application.Users.DTOs.Request;
using MediatR;

namespace Application.Identity.Commands
{
    public class RegisterCommand : IRequest<IdentityResponse>
    {
        public required CreateUserRequest CreateUser { get; set; }
        public required string RoleName { get; set; }
    }
}