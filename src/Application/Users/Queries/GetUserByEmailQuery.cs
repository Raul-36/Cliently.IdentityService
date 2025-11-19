using Application.Users.DTOs.Response;
using MediatR;

namespace Application.Users.Queries
{
    public class GetUserByEmailQuery : IRequest<UserResponse>
    {
        public required string Email { get; set; }
    }
}
