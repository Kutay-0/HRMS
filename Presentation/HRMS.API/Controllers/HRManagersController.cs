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
    public class HRManagersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HRManagersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("HR Manager Service is running");
        }

        [HttpGet("whoami")]
        public IActionResult WhoAmI([FromServices] IHttpContextAccessor acc)
        {
            var u = acc.HttpContext?.User;
            var roles = u?.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                                 .Select(c => c.Value).ToArray();
            return Ok(new
            {
                isAuth = u?.Identity?.IsAuthenticated ?? false,
                roles,
                userId = u?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
                email = u?.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value
                     ?? u?.FindFirst("email")?.Value
            });
        }

        [HttpPost("register")]
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

        [AllowAnonymous]
        [HttpPatch("update")]
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


        [AllowAnonymous]
        [HttpGet("company/{companyId}/hrmanagers")]
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
