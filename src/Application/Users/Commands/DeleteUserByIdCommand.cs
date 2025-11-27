using System;
using MediatR;

namespace Application.Users.Commands
{
    public class DeleteUserByIdCommand : IRequest
    {
      public required Guid Id { get; set; }   
    }
}