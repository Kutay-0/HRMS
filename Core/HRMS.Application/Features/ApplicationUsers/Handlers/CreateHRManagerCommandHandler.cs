using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Features.ApplicationUsers.Commands;
using HRMS.Application.Repositories;
using HRMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HRMS.Application.Features.ApplicationUsers.Handlers
{
    public class CreateHRManagerCommandHandler : IRequestHandler<CreateHRManagerCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateHRManagerCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> Handle(CreateHRManagerCommand command, CancellationToken cancellationToken)
        {
            var existUser = await _userManager.FindByEmailAsync(command.Email);
            if(existUser != null)
            {
                throw new Exception("Bu e-posta adresiyle zaten bir hesap oluşturulmuş!");
            }

            var hrManager = new ApplicationUser
            {
                UserName = command.FullName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                CompanyId = command.CompanyId
            };

            var result = await _userManager.CreateAsync(hrManager, command.Password);
            await _userManager.AddToRoleAsync(hrManager, "HRManager");
            
            return hrManager.Id;
        }
    }
}
