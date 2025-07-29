using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityTestController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityTestController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet("create-role")]
        public async Task<IActionResult> CreateRoles()
        {
            string[] roles = { "HRMSAdmin", "AppUser", "HRManager" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            return Ok("Roller Başarıyla Eklendi.");
        }
    }
}
