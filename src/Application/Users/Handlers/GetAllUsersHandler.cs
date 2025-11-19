using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Users.DTOs.Response;
using Application.Users.Queries;
using Application.Users.Services.Base;
using AutoMapper;
using MediatR;

namespace Application.Users.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponse>>
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public GetAllUsersHandler(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await userService.GetAllUsersAsync();
            var mapped = mapper.Map<IEnumerable<UserResponse>>(users);
            return mapped;
        }
    }
}
