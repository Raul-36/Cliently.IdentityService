using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Users.DTOs.Response;
using Application.Users.Queries;
using Application.Users.Services.Base;
using AutoMapper;
using Core.Users.Entities.Base;
using MediatR;

namespace Application.Users.Handlers
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, Result<UserResponse>>
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;


        public GetUserByEmailHandler(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<Result<UserResponse>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await this.userService.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return Result<UserResponse>.Failure("User not found");
            }
            var mapped = mapper.Map<UserResponse>(user);
            return Result<UserResponse>.Success(mapped);
        }
    }
}
