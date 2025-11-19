using System;
using MediatR;

namespace Application.Users.Commands
{
    public class DeleteUserByIdCommand : IRequest<bool>
    {
      public required Guid Id { get; set; }   
    }
}