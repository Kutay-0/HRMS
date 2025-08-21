using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HRMS.API.Settings;
using HRMS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using HRMS.Application.Features.ApplicationUsers.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using HRMS.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor; 
        public AuthController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _mediator.Send(new LoginUserCommand
            {
                Email = dto.Email,
                Password = dto.Password
            });
            return Ok(result);
        }

        [Authorize(Roles = "HRManager,Admin,User")]
        [HttpGet("whoami")]
        public async Task<IActionResult> WhoAmI()
        {
            var u = HttpContext?.User;
            if (u?.Identity?.IsAuthenticated != true)
                return Unauthorized(new { isAuth = false });

            // Rolleri hem "role" hem de ClaimTypes.Role'dan dene
            var roles = u.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();
            if (roles.Length == 0)
                roles = u.FindAll("role").Select(c => c.Value).ToArray();

            // userId için birden çok olası claim ismi:
            var userId =
                   u.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? u.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value
                ?? u.FindFirst("Id")?.Value;

            // email için de esnek oku:
            var email =
                   u.FindFirst(ClaimTypes.Email)?.Value
                ?? u.FindFirst("email")?.Value
                ?? u.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email)?.Value
                ?? u.FindFirst("Email")?.Value;

            return Ok(new
            {
                isAuth = true,
                roles,
                userId,
                email
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("ping")]                          // -> GET /api/Auth/ping
        public IActionResult Ping() => Ok("pong");
    } 
}
