using System.Threading;
using System.Threading.Tasks;
using Application.Users.DTOs.Response;
using Application.Users.Queries;
using Application.Users.Services.Base;
using AutoMapper;
using MediatR;

namespace Application.Users.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public GetUserByIdHandler(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByIdAsync(request.Id);
            var mapped = mapper.Map<UserResponse>(user);
            return mapped;
        }
    }
}
