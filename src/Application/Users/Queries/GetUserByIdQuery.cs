using System;
using Application.Users.DTOs.Response;
using MediatR;

namespace Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public Guid Id { get; set; }
    }
}
