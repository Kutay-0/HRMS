using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.DTOs;
using HRMS.Application.Features.ApplicationUsers.Queries;
using HRMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HRMS.Application.Features.ApplicationUsers.Handlers
{
    public class GetHRManagerByCompanyIdQueryHandler : IRequestHandler<GetHRManagerByCompanyIdQuery, List<HRManagerDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetHRManagerByCompanyIdQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<HRManagerDto>> Handle(GetHRManagerByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.Where(u => u.CompanyId == request.CompanyId).ToList();

            var hrManagers = new List<HRManagerDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("HRManager"))
                {
                    hrManagers.Add(new HRManagerDto
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Role = "HRManager",
                        CreatedAt = user.CreatedAt
                    });
                }
            }
            return hrManagers;
        }
    }
}
