using System.Collections.Generic;
using Application.Users.DTOs.Response;
using MediatR;

namespace Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserResponse>>
    {
    }
}
