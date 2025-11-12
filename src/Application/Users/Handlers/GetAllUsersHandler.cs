using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Users.DTOs.Response;
using Application.Users.Queries;
using Application.Users.Services.Base;
using AutoMapper;
using Core.Users.Entities;
using Core.Users.Entities.Base;
using MediatR;

namespace Application.Users.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserResponse>>>
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;


        public GetAllUsersHandler(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await userService.GetAllUsersAsync();
            var mapped = mapper.Map<IEnumerable<UserResponse>>(users);
            return Result<IEnumerable<UserResponse>>.Success(mapped);
        }
    }
}
