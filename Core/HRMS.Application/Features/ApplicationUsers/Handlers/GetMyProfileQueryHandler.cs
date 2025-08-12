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
    public class GetMyProfileQueryHandler : IRequestHandler<GetMyProfileQuery, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetMyProfileQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDto> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            var role = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
