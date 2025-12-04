using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Identity.Commands;
using Application.Identity.DTOs.Response;
using Application.Roles.Services.Base;
using Application.Tokens.DTOs.Request;
using Application.Tokens.Services.Base;
using Application.UserRoles.Services.Base;
using Application.Users.Commands;
using Application.Users.DTOs.Request;
using AutoMapper;
using MediatR;

namespace Application.Identity.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, IdentityResponse>
    {
        private readonly IRoleService roleService;
        private readonly IUserRoleService userRoleService;
        private readonly ITokenGeneratorService tokenGenerator;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public RegisterHandler(IRoleService roleService, IUserRoleService userRoleService, ITokenGeneratorService tokenGenerator, IMediator mediator, IMapper mapper)
        {
            this.roleService = roleService;
            this.userRoleService = userRoleService;
            this.tokenGenerator = tokenGenerator;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<IdentityResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var createUserCommand = new CreateUserCommand
            {
                CreateUser = request.CreateUser
            };

            var user = await mediator.Send(createUserCommand, cancellationToken);

            var roles = new List<string>();
            if (!string.IsNullOrEmpty(request.RoleName))
            {
                var role = await roleService.GetRoleByNameAsync(request.RoleName);
                await userRoleService.AssignRoleToUserAsync(user.Id, role.Id);
                roles.Add(request.RoleName);
            }

            var generateJWTReq = new GenerateJWTTokenRequest
            {
                User = mapper.Map<JWTUserRequest>(user),
                Roles = roles
            };
            var token = tokenGenerator.GenerateJWTToken(generateJWTReq);

            var response = new IdentityResponse
            {
                User = user,
                Token = token
            };

            return response;
        }
    }
}