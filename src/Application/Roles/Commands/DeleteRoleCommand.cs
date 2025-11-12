using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using MediatR;

namespace Application.Roles.Commands
{
    public class DeleteRoleCommand : IRequest<Result<bool>>
    {
        public Guid RoleId { get; set; }
    }
}
