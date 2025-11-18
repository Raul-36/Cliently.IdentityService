using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Application.Users.Queries;
using Application.Users.Commands;
using Application.Users.DTOs.Request;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            Console.WriteLine("GetAllUsers called");
            var query = new GetAllUsersQuery();
            var result = await mediator.Send(query);
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var result = await mediator.Send(query);
            if (result.IsSuccess && result.Value != null)
            {
                return Ok(result.Value);
            }
            return NotFound();
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        {   
            var command = new UpdateUserCommand { request = request };
            var result = await mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteUserByIdCommand { Id = id };
            var result = await mediator.Send(command);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
    }
}