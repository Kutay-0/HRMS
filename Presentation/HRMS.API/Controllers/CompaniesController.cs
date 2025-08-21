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

        [HttpPost("companies/create")]
        public async Task<IActionResult> Create([FromBody] CreateCompanyCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("companies/update")]
        public async Task<IActionResult> Update([FromBody] UpdateCompanyCommand command)
        {
            try
            {
                var updatedId = await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("company/delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new DeleteCompanyCommand { Id = id };
                var deletedId = await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("companies")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = new GetAllCompaniesQuery();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] GetCompanyByFilterQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/jobpostings")]
        public async Task<IActionResult> GetJobPostingsByCompanyId(int id)
        {
            try
            {
                var query = new GetJobPostingsByCompanyIdQuery { CompanyId = id };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
