using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Identity.Commands;
using Application.Identity.DTOs.Response;
using Application.Roles.Services.Base;
using Application.Tokens.Services.Base;
using Application.UserRoles.Services.Base;
using Application.Identity.Services.Base;
using AutoMapper;
using MediatR;
using Application.Users.Queries;
using Application.Users.DTOs.Request;
using Application.Tokens.DTOs.Request;
using Application.Identity.Exceptions;
using Application.Users.Exceptions;
using Core.Users.Entities.Base;
using Application.Users.DTOs.Response;

namespace Application.Identity.Handlers
{
    public class SignInHandler : IRequestHandler<SignInCommand, IdentityResponse>
    {
        private readonly IRoleService roleService;
        private readonly IUserRoleService userRoleService;
        private readonly ISignInService signInService;
        private readonly ITokenGeneratorService tokenGenerator;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public SignInHandler(ISignInService signInService, IRoleService roleService, IUserRoleService userRoleService, ITokenGeneratorService tokenGenerator, IMediator mediator, IMapper mapper)
        {
            this.roleService = roleService;
            this.userRoleService = userRoleService;
            this.tokenGenerator = tokenGenerator;
            this.signInService = signInService;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<IdentityResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var getUserByEmailQuery = new GetUserByEmailQuery
            {
                Email = request.request.Email
            };

            UserResponse user;
            try
            {
                user = await mediator.Send(getUserByEmailQuery, cancellationToken);
            }
            catch (UserNotFoundException)
            {
                throw new SignInException();
            }

            var passwordCheck = await signInService.PasswordSignInAsync(user.Email, request.request.Password);
            if (passwordCheck == false)
                throw new SignInException();

            var userRoles = await userRoleService.GetByUserId(user.Id);
            
            var rolesIds = userRoles.Select(r => r.RoleId);
            
            var roles = await roleService.GetRoleByIdAsync(rolesIds);
            
            var generateJWTReq = new GenerateJWTTokenRequest
            {
               User = mapper.Map<JWTUserRequest>(user),
               Roles = roles.Select(r => r.Name ?? throw new InvalidOperationException("Role name is null"))
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