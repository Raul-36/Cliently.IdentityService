using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using MediatR;

namespace Application.Roles.Commands
{
    public class CreateRoleCommand : IRequest<Result<bool>>
    {
        public required string Name { get; set; }
    }
}