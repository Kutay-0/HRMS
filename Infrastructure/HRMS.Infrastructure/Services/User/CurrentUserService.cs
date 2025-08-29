using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Abstractions.User;
using Microsoft.AspNetCore.Http;

namespace HRMS.Infrastructure.Services.User
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId
            => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
       
        public string Email
            => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

        public string[] Roles
            => _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role)
                .Select(role => role.Value)
                .ToArray() ?? Array.Empty<string>();
    }
}
