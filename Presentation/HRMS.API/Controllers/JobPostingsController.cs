using HRMS.Application.Features.JobPostings.Commands;
using HRMS.Application.Features.JobPostings.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostingsController : ControllerBase
    {
        private readonly IMediator Mediator;
        public JobPostingsController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobPostingCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteJobPostingCommand { JobPostingId = id };
            var deletedId = await Mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateJobPostingCommand command)
        {
            var updatedId = await Mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllJobPostingQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] GetJobPostingByFilterQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
