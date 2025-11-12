using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Roles.Services.Base;
using Application.UserRoles.Services.Base;
using Application.Users.Commands;
using Application.Users.DTOs.Response;
using Application.Users.Services.Base;
using AutoMapper;
using Core.Roles.Entities.Base;
using Core.Users.Entities;
using Core.Users.Entities.Base;
using MediatR;

namespace Application.Users.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<UserResponse>>
    {
        private readonly IUserService userService;
       private readonly IMapper mapper;
        public CreateUserHandler(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }
        public async Task<Result<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.CreateUserAsync(request.request);

            var mapped = mapper.Map<UserResponse>(user);
            return Result<UserResponse>.Success(mapped);
        }
    }
}