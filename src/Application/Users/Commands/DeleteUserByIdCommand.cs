using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using MediatR;

namespace Application.Users.Commands
{
    public class DeleteUserByIdCommand : IRequest<Result<bool>>
    {
      public required Guid Id { get; set; }   
    }
}