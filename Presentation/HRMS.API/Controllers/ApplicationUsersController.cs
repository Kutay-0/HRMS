using HRMS.Application.Features.ApplicationUsers.Commands;
using HRMS.Application.Features.ApplicationUsers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ApplicationUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserCommand command)
        {
            var userId = await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("hrmanager/register")]
        public async Task<IActionResult> RegisterHRManager([FromBody] CreateHRManagerCommand command)
        {
            var userId = await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var command = new DeleteUserCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("hrmanager/{id}")]
        public async Task<IActionResult> DeleteHRManager(string id)
        {
            var command = new DeleteHRManagerCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("user/update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHRManagerByCompanyId(int Id)
        {
            var query = new GetHRManagerByCompanyIdQuery { CompanyId = Id };
            var result = await _mediator.Send(query);
            return Ok();
        }
    }
}
