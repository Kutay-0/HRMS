using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Features.ApplicationUsers.Commands;
using HRMS.Application.Features.ApplicationUsers.Validators;
using HRMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HRMS.Application.Features.ApplicationUsers.Handlers
{
    public class UpdateHRManagerCommandHandler : IRequestHandler<UpdateHRManagerCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateHRManagerCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(UpdateHRManagerCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.Id);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı! Lütfen geçerli bir kullanıcı ID'si giriniz.");
            }

            var validationMessage = UpdateHRManagerValidator.Validate(command);
            if (!string.IsNullOrWhiteSpace(validationMessage))
            {
                throw new Exception(validationMessage);
            }

            if(!string.IsNullOrWhiteSpace(command.FullName))
            {
                user.FullName = command.FullName.Trim();
            }

            if(!string.IsNullOrWhiteSpace(command.Email))
            {
                user.Email = command.Email.Trim();
                user.UserName = command.Email.Trim();
            }

            if(!string.IsNullOrWhiteSpace(command.PhoneNumber))
            {
                user.PhoneNumber = command.PhoneNumber.Trim();
            }

            if(command.CompanyId > 0)
            {
                user.CompanyId = command.CompanyId;
            }
            
            if(!string.IsNullOrWhiteSpace(command.NewPassword))
            {
                var passwordChangeResult = await _userManager.ChangePasswordAsync(user, command.Password, command.NewPassword);
                if (!passwordChangeResult.Succeeded)
                {
                    throw new Exception("Parola değiştirilemedi.");
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                throw new Exception("Kullanıcı güncellenirken bir hata meydana geldi.");
            }

            return user.Id;
        }
    }
}
