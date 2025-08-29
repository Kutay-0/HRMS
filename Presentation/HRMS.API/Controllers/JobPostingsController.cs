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

        [HttpPost("create")]
        public async Task<IActionResult> CreateJobPosting([FromBody] CreateJobPostingCommand command)
        {
            try
            {
                var id = await Mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteJobPosting(int id)
        {
            try
            {
                var command = new DeleteJobPostingCommand { JobPostingId = id };
                var deletedId = await Mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("update/{id:int}")]
        public async Task<IActionResult> UpdateJobPosting(int id, [FromBody] UpdateJobPostingCommand command)
        {
            try
            {
                command.JobPostingId = id;
                var updatedId = await Mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("select")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = new GetAllJobPostingQuery();
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] GetJobPostingByFilterQuery query)
        {
            try
            {
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("~/api/companies/{companyId:int}/job-postings")]
        public async Task<IActionResult> GetByCompany([FromRoute] int companyId,[FromQuery] GetJobPostingByFilterQuery query)
        {
            query.CompanyId = companyId;
            if (query.IsActive is null) query.IsActive = true;
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
