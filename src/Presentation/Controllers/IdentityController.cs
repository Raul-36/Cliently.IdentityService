using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Identity.Commands;
using Application.Identity.DTOs.Request;
using Application.Users.DTOs.Request;
using Application.Common.Exceptions;
using Application.Users.Exceptions;
using Application.Roles.Exceptions;
using Application.Identity.Exceptions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator mediator;

        public IdentityController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserRequest request)
        {
            try
            {
                var command = new RegisterCommand { CreateUser = request, RoleName = "User" };
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            try
            {
                var command = new SignInCommand { request = request };
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (SignInException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}