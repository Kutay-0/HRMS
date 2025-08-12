using HRMS.Application.Features.Candidaties.Commands;
using HRMS.Application.Features.Candidaties.Handlers;
using HRMS.Application.Features.Candidaties.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCandidate([FromBody] CreateCandidateCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCandidate([FromBody] DeleteCandidateCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CandidateByJobPostingId/{jobPostingId:int}")]
        public async Task<IActionResult> GetCandidate([FromRoute] int jobPostingId,[FromQuery] DateTime? createdAfter,[FromQuery] DateTime? createdBefore,[FromQuery] string? emailKeyword)
        {
            try
            {
                var query = new GetCandidateByFilterQuery
                {
                    JobPostingId = jobPostingId,
                    CreatedAfter = createdAfter,
                    CreatedBefore = createdBefore,
                    EmailKeyword = emailKeyword
                };

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
