using Application.Users.DTOs.Request;
using Application.Users.DTOs.Response;
using MediatR;

namespace Application.Users.Commands
{
    public class UpdateUserCommand : IRequest<UserResponse>
    {
        public required UpdateUserRequest UpdateUser { get; set; }
    }
}
