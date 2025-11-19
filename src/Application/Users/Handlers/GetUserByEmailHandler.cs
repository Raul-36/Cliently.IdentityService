using System.Threading;
using System.Threading.Tasks;
using Application.Users.DTOs.Response;
using Application.Users.Queries;
using Application.Users.Services.Base;
using AutoMapper;
using MediatR;

namespace Application.Users.Handlers
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserResponse>
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public GetUserByEmailHandler(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<UserResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByEmailAsync(request.Email);
            var mapped = mapper.Map<UserResponse>(user);
            return mapped;
        }
    }
}
