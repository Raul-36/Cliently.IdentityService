using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Identity.Commands;
using Application.Identity.DTOs.Request;
using Microsoft.AspNetCore.Identity.Data;
using Application.Users.DTOs.Request;

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
            var command = new RegisterCommand { CreateUser = request, RoleName = "User"};
            var result = await mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var command = new SignInCommand { request = request };
            
            var result = await mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return Unauthorized(result.Errors);
        }
    }
}