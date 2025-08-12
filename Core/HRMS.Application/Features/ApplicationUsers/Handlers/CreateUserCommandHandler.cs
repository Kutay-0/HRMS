using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.DTOs;
using HRMS.Application.Features.ApplicationUsers.Commands;
using HRMS.Application.Features.ApplicationUsers.Validators;
using HRMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace HRMS.Application.Features.ApplicationUsers.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public CreateUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var existUser = await _userManager.FindByEmailAsync(request.Email);
            if(existUser != null)
            {
                throw new Exception("Bu e-posta adresiyle zaten bir hesap oluşturulmuş!");
            }

            var validationMessage = CreateUserValidator.Validate(request);
            if (!string.IsNullOrEmpty(validationMessage))
            {
                throw new Exception(validationMessage);
            }

            var user = new ApplicationUser
            {
                FullName = request.FullName,
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                CompanyId = null,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Kullanıcıyı oluştururken bir hata meydana geldi.");
            }
            
            var addRole = await _userManager.AddToRoleAsync(user, "User");
            if (!addRole.Succeeded)
            {
                throw new Exception("Kullanıcıya rol eklenirken bir hata meydana geldi.");
            }

            return user.Id;
        }
    }
}
