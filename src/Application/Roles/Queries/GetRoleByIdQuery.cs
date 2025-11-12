using System;
using Application.Common;
using Core.Roles.Entities.Base;
using MediatR;

namespace Application.Roles.Queries
{
    public class GetRoleByIdQuery : IRequest<Result<IRole>>
    {
        public Guid Id { get; set; }
    }
}
