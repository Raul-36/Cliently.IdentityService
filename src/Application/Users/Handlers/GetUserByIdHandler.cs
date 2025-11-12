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
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Result<UserResponse>>
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;


        public GetUserByIdHandler(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByIdAsync(request.Id);
            if (user == null)
            {
                return Result<UserResponse>.Failure("User not found");
            }
            var mapped = mapper.Map<UserResponse>(user);
            return Result<UserResponse>.Success(mapped);
        }
    }
}
