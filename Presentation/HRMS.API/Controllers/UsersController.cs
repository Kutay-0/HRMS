using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using HRMS.Application.Features.ApplicationUsers.Commands;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserCommand command)
        {
            try
            {
                var userId = await _mediator.Send(command);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                var messages = ex.Message.Split('|').ToList();
                return BadRequest(messages);
            }
        }
    }
}
