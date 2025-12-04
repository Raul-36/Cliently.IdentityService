using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Application.Users.Queries;
using Application.Users.Commands;
using Application.Users.DTOs.Request;
using System.Security.Claims;
using Application.Users.Exceptions;
using Application.Common.Exceptions;
using Presentation.Options;
using Presentation.Consts;

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
        [Authorize (Roles = DefaultRoles.Admin)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var query = new GetAllUsersQuery();
                var result = await mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var currentUserIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(currentUserIdString, out var currentUserId) == false)
                return Unauthorized();

            var isAdmin = User.IsInRole("Admin");

            if (isAdmin == false && currentUserId != id)
                return Forbid();


            try
            {
                var query = new GetUserByIdQuery { Id = id };
                var result = await mediator.Send(query);
                return Ok(result);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        {   
            var currentUserIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(currentUserIdString, out var currentUserId) == false)
                return Unauthorized();

            if (currentUserId != request.Id)
                return Forbid();

            try
            {
                var command = new UpdateUserCommand { UpdateUser = request };
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var currentUserIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(currentUserIdString, out var currentUserId) == false)
            {
                return Unauthorized();
            }

            var isAdmin = User.IsInRole("Admin");

            if (isAdmin == false && currentUserId != id)
            {
                return Forbid();
            }
            if (isAdmin == true && currentUserId == id)
            {
                return Forbid();
            }

            var command = new DeleteUserByIdCommand { Id = id };
            await mediator.Send(command);
            return NoContent();
        }
    }
}