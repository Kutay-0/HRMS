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
            var hrManagers = await _userManager.GetUsersInRoleAsync("HRManager");

            var  filtered = hrManagers.Where(user => user.CompanyId == request.CompanyId).OrderBy(user => user.FullName).ToList();

            var list = filtered.Select(user => new HRManagerDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = "HRManager",
                CreatedAt = user.CreatedAt
            }).ToList();

            return list;
        }
    }
}
