using Application.Users.DTOs.Request;
using Application.Users.DTOs.Response;
using MediatR;

namespace Application.Users.Commands
{
    public class CreateUserCommand : IRequest<UserResponse>
    {
        public required CreateUserRequest request { get; set; }
    }
}