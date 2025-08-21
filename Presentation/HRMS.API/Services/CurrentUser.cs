using System.Security.Claims;
using HRMS.Application.Abstractions;

namespace HRMS.API.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _ctx;

        public CurrentUser(IHttpContextAccessor ctx)
        {
            _ctx = ctx;
        }

        private ClaimsPrincipal? ClaimsPrincipal => _ctx.HttpContext?.User;

        public string? UserId
        {
            get => ClaimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string? Email
        {
            get => ClaimsPrincipal?.FindFirstValue(ClaimTypes.Email);
        }

        public int? CompanyId
        {
            get
            {
                var value = ClaimsPrincipal?.FindFirst("companyId")?.Value;
                return int.TryParse(value, out var companyId) ? companyId : null;
            } 
        }

        public string? FullName
        {
            get => ClaimsPrincipal?.FindFirstValue(ClaimTypes.Name);
        }

        public bool IsAuthenticated
        {
            get => ClaimsPrincipal?.Identity?.IsAuthenticated ?? false;
        }

        public bool IsInRole(string role)
        {
            return ClaimsPrincipal?.IsInRole(role) ?? false;
        }
    }
}
