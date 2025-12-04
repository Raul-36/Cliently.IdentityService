using System.Threading;
using System.Threading.Tasks;
using Application.Users.Commands;
using Application.Users.DTOs.Response;
using Application.Users.Services.Base;
using AutoMapper;
using MediatR;

namespace Application.Users.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserResponse>
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public CreateUserHandler(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.CreateUserAsync(request.CreateUser);
            var mapped = mapper.Map<UserResponse>(user);
            return mapped;
        }
    }
}