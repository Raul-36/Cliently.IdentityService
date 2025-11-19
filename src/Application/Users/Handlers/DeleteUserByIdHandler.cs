using System.Threading;
using System.Threading.Tasks;
using Application.Users.Commands;
using Application.Users.Services.Base;
using MediatR;

namespace Application.Users.Handlers
{
    public class DeleteUserByIdHandler : IRequestHandler<DeleteUserByIdCommand, bool>
    {
        private readonly IUserService userService;

        public DeleteUserByIdHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<bool> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            return await userService.DeleteUserByIdAsync(request.Id);
        }
    }
}