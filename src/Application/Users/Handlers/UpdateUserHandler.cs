using System.Threading;
using System.Threading.Tasks;
using Application.Users.Commands;
using Application.Users.DTOs.Response;
using Application.Users.Services.Base;
using AutoMapper;
using MediatR;

namespace Application.Users.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserResponse>
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UpdateUserHandler(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByIdAsync(request.UpdateUser.Id);

            user.Email = request.UpdateUser.Email;

            var updatedUser = await userService.UpdateUserAsync(user);

            var mapped = mapper.Map<UserResponse>(updatedUser);
            return mapped;
        }
    }
}
