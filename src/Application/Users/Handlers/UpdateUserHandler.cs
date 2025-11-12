using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Users.Commands;
using Application.Users.DTOs.Response;
using Application.Users.Services.Base;
using AutoMapper;
using Core.Users.Entities.Base;
using MediatR;

namespace Application.Users.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<UserResponse>>
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;


        public UpdateUserHandler(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<Result<UserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await this.userService.GetUserByIdAsync(request.request.Id);
            if (user == null)
            {
                return Result<UserResponse>.Failure("User not found");
            }

            user.Email = request.request.Email;

            var updatedUser = await this.userService.UpdateUserAsync(user);
            var mapped = mapper.Map<UserResponse>(updatedUser);
            return Result<UserResponse>.Success(mapped);
        }
    }
}
