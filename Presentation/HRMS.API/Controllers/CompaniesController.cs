using HRMS.Application.Features.Companies.Commands;
using HRMS.Application.Features.Companies.Queries;
using HRMS.Application.Features.JobPostings.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCompanyCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCompanyCommand command)
        {
            var updatedId = await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCompanyCommand { Id = id };
            var deletedId = await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCompaniesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] GetCompanyByFilterQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("{id}/jobpostings")]
        public async Task<IActionResult> GetJobPostingsByCompanyId(int id)
        {
            var query = new GetJobPostingsByCompanyIdQuery { CompanyId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
