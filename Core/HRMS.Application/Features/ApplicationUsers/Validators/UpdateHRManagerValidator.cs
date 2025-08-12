using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Features.ApplicationUsers.Commands;

namespace HRMS.Application.Features.ApplicationUsers.Validators
{
    public static class UpdateHRManagerValidator
    {
        public static string Validate(UpdateHRManagerCommand command)
        {
            var errors = new List<string>();

            var fullName = command.FullName?.Trim();
            var email = command.Email?.Trim();
            var phoneNumber = command.PhoneNumber?.Trim();
            var password = command.Password?.Trim();
            var newpassword = command.NewPassword?.Trim();

            if (!string.IsNullOrWhiteSpace(fullName))
            {
                if (fullName.Length > 50)
                    errors.Add("Ad-soyad alanı 50 karakterden uzun olamaz.");

                if (!fullName.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                    errors.Add("Ad soyad sadece harf ve boşluk içerebilir.");
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                if (!email.Contains("@") || !email.Contains("."))
                    errors.Add("Geçersiz email formatı.");
            }

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                if (!phoneNumber.All(char.IsDigit) || phoneNumber.Length < 10 || phoneNumber.Length > 15)
                    errors.Add("Telefon numarası sadece rakamlardan oluşmalı ve 10-15 karakter uzunluğunda olmalıdır.");
            }

            if (!string.IsNullOrWhiteSpace(command.NewPassword))
            {
                if (string.IsNullOrWhiteSpace(command.Password))
                    errors.Add("Yeni bir parola oluşturabilmek için mevcut parolanızı girmeniz gerekmektedir.");

                if (newpassword.Length < 6 || newpassword.Length > 16)
                    errors.Add("Yeni parola 6-16 karakter arasında olmalıdır.");

                if (!(newpassword.Any(char.IsUpper) && newpassword.Any(char.IsLower) && newpassword.Any(char.IsDigit)))
                    errors.Add("Yeni parola en az bir büyük, bir küçük harf ve bir rakam içermeli.");

                if (newpassword == command.Password)
                    errors.Add("Yeni parola mevcut parolanızla aynı olamaz.");
            }

            return string.Join(" | ", errors);
        }
    }
}
