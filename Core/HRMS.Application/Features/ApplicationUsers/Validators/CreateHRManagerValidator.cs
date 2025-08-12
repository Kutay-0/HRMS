using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Features.ApplicationUsers.Commands;

namespace HRMS.Application.Features.ApplicationUsers.Validators
{
    public static class CreateHRManagerValidator
    {
        public static string Validate(CreateHRManagerCommand command)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(command.FullName))
            {
                errors.Add("Ad soyad alanı boş bırakılamaz.");
            }
            else
            {
                if (command.FullName.Length > 50)
                {
                    errors.Add("Ad soyad alanı 50 karakterden uzun olamaz.");
                }
                if (!command.FullName.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                {
                    errors.Add("Ad soyad alanı sadece harf ve boşluk içerebilir.");
                }
            }

            if (string.IsNullOrWhiteSpace(command.Email))
            {
                errors.Add("Email alanı boş bırakılamaz.");
            }
            else
            {
                if (!command.Email.Contains("@") || !command.Email.Contains("."))
                {
                    errors.Add("Geçersiz email formatı.");
                }
            }

            if (string.IsNullOrEmpty(command.PhoneNumber))
            {
                errors.Add("Telefon numarası alanı boş bırakılamaz.");
            }
            else if (!command.PhoneNumber.All(char.IsDigit) || command.PhoneNumber.Length < 10 || command.PhoneNumber.Length > 15)
            {
                errors.Add("Telefon numarası sadece rakamlardan oluşmalı ve 10-15 karakter uzunluğunda olmalıdır.");
            }

            if (string.IsNullOrWhiteSpace(command.Password))
            {
                errors.Add("Şifre alanı boş bırakılamaz.");
            }
            else
            {
                if (command.Password.Length < 6 || command.Password.Length > 16)
                {
                    errors.Add("Parola 6-16 karakter uzunluğunda olmalıdır.");
                }
                if (!command.Password.Any(char.IsUpper) || !command.Password.Any(char.IsLower) || !command.Password.Any(char.IsDigit))
                {
                    errors.Add("Parola en az bir büyük harf, bir küçük harf ve bir rakam içermelidir.");
                }
            }

            return string.Join(" | ", errors);

        }
    }
}
