using HRMS.Application.Features.ApplicationUsers.Commands;
using HRMS.Application.Features.ApplicationUsers.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HRManagersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HRManagersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("HRManager/Register")]
        public async Task<IActionResult> RegisterHRManager([FromBody] CreateHRManagerCommand command)
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

        [HttpPatch("HRManager/Update")]
        public async Task<IActionResult> UpdateHRManager([FromBody] UpdateHRManagerCommand command)
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


        [HttpGet("company/{companyId}/hr-managers")]
        public async Task<IActionResult> GetHRManagersByCompany(int companytId)
        {
            try
            {
                var query = new GetHRManagerByCompanyIdQuery { CompanyId = companytId };
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
