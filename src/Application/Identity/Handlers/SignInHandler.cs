using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Identity.Commands;
using Application.Identity.DTOs.Response;
using Application.Roles.Services.Base;
using Application.Tokens.Services.Base;
using Application.UserRoles.Services.Base;
using Application.Identity.Services.Base;
using AutoMapper;
using MediatR;
using Application.Users.Queries;
using Application.Users.DTOs.Response;
using Application.Users.DTOs.Request;
using Application.Tokens.DTOs.Request;

namespace Application.Identity.Handlers
{
    public class SignInHandler : IRequestHandler<SignInCommand, Result<IdentityResponse>>
    {
        private readonly IRoleService roleService;
        private readonly IUserRoleService userRoleService;
        private readonly ISignInService signInService;
        private readonly ITokenGeneratorService tokenGenerator;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public SignInHandler(ISignInService signInService,IRoleService roleService, IUserRoleService userRoleService, ITokenGeneratorService tokenGenerator, IMediator mediator, IMapper mapper)
        {
            this.roleService = roleService;
            this.userRoleService = userRoleService;
            this.tokenGenerator = tokenGenerator;
            this.signInService = signInService;
            this.mediator = mediator;
            this.mapper = mapper;
        }
        public async Task<Result<IdentityResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var getUserByEmailQuery = new GetUserByEmailQuery
            {
                Email = request.request.Email
            };

            var getUserResult = await mediator.Send(getUserByEmailQuery);
            if (getUserResult.IsSuccess == false)
            {
                return Result<IdentityResponse>.Failure(getUserResult.Errors
                ?? new List<string>() { "Unknown error at getting user" });
            }

            var user = getUserResult.Value;
            if (user == null)
            {
                return Result<IdentityResponse>.Failure($"Unknown error, user is null");
            }

            var passwordCheckResult = await signInService.PasswordSignInAsync(user.Email, request.request.Password);
            if (passwordCheckResult == false)
                return Result<IdentityResponse>.Failure("Incorrect email or password");

            var getUserRolesResult = await userRoleService.GetByUserId(user.Id);
            if (getUserRolesResult.IsSuccess == false)
            {
                return Result<IdentityResponse>.Failure(getUserRolesResult.Errors
                ?? new List<string>() { "Unknown error at getting user roles" });
            }

            var userRoles = getUserRolesResult.Value;
            if (userRoles == null)
            {
                return Result<IdentityResponse>.Failure("Unknown error, user roles is null");
            }

            var rolesIds = userRoles.Select(r => r.RoleId);
            var getRolesResult = await roleService.GetRoleByIdAsync(rolesIds);
            if (getRolesResult.IsSuccess == false)
            {
                return Result<IdentityResponse>.Failure(getRolesResult.Errors
                ?? new List<string>() { "Unknown error at getting roles" });
            }

            var roles = getRolesResult.Value;
            if (roles == null)
            {
                return Result<IdentityResponse>.Failure("Unknown error, roles is null");
            }
            

            var cenerateJWTReq = new GenerateJWTTokenRequest
            {
               User = mapper.Map<JWTUserRequest>(user),
               Roles = roles.Select(r => r.Name ?? throw new InvalidOperationException("Role name is null"))
            };
            var token = this.tokenGenerator.GenerateJWTToken(cenerateJWTReq);

            var response = new IdentityResponse
            {
                User = user,
                Token = token
            };

            return Result<IdentityResponse>.Success(response);
        }
    }
}