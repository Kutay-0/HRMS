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
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HRMS.Application.Features.ApplicationUsers.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validationMessage = UpdateUserValidator.Validate(request);
            
            if (!string.IsNullOrWhiteSpace(validationMessage))
                throw new Exception(validationMessage);

            var user = await _userManager.FindByIdAsync(request.Id);
            if(user == null)
            {
                throw new Exception("Kullanıcı bulunamadı! Lütfen geçerli bir kullanıcı ID'si giriniz.");
            }

            if (!string.IsNullOrWhiteSpace(request.FullName))
            {
                user.FullName = request.FullName.Trim();
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                user.Email = request.Email.Trim();
                user.UserName = request.Email.Trim();
            }

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                user.PhoneNumber = request.PhoneNumber.Trim();
            }
             
            if (!string.IsNullOrWhiteSpace(request.NewPassword))
            {
                var passwordChangeResult = await _userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);
                if(!passwordChangeResult.Succeeded)
                {
                    throw new Exception("Parola değiştirilemedi.");
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
                throw new Exception($"Kullanıcı güncellenirken hata oluştu: {errors}");
            }

            return user.Id;
        }
    }
}
