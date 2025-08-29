using HRMS.Application.Features.CompanyRequests.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("companyrequest/create")]
        public async Task<IActionResult> CreateCompanyRequest([FromBody] CreateCompanyRequestCommand command)
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
    }
}
