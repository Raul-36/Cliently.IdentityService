using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Identity.Commands;
using Application.Identity.DTOs.Response;
using Application.Roles.Services.Base;
using Application.Tokens.DTOs.Request;
using Application.Tokens.Services.Base;
using Application.UserRoles.Services.Base;
using Application.Users.Commands;
using Application.Users.DTOs.Request;
using Application.Users.DTOs.Response;
using AutoMapper;
using Core.Roles.Entities.Base;
using MediatR;

namespace Application.Identity.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<IdentityResponse>>
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

        public async Task<Result<IdentityResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var createUserCommand = new CreateUserCommand
            {
                request = request.createUser
            };

            var createUserResult = await mediator.Send(createUserCommand);

            if (createUserResult.IsSuccess == false)
            {
                return Result<IdentityResponse>.Failure(createUserResult.Errors
                ?? new List<string>() { "Unknown error" });
            }

            var user = createUserResult.Value;
            if (user == null)
            {
                return Result<IdentityResponse>.Failure("Unknown error");
            }

            List<string> roles = new List<string>();
            if (request.RoleName != null)
            {
                IRole? role = await roleService.GetRoleByNameAsync(request.RoleName);

                if (role == null)
                    return Result<IdentityResponse>.Failure($"Role '{request.RoleName}' not found.");

                await userRoleService.AssignRoleToUserAsync(user.Id, role.Id);
                roles.Add(request.RoleName);
            }

            var userResponse = mapper.Map<UserResponse>(user);

            var cenerateJWTReq = new GenerateJWTTokenRequest
            {
               User = mapper.Map<JWTUserRequest>(user),
               Roles = roles
            };
            var token = this.tokenGenerator.GenerateJWTToken(cenerateJWTReq);


            var response = new IdentityResponse
            {
                User = userResponse,
                Token = token
            };

            return Result<IdentityResponse>.Success(response);
        }
    }
}