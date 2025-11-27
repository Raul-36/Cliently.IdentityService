using System.Threading;
using System.Threading.Tasks;
using Application.Users.Commands;
using Application.Users.Services.Base;
using MediatR;

namespace Application.Users.Handlers
{
    public class DeleteUserByIdHandler : IRequestHandler<DeleteUserByIdCommand>
    {
        private readonly IUserService userService;

        public DeleteUserByIdHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
         await userService.DeleteUserByIdAsync(request.Id);
        }
    }
}