using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Features.Candidaties.Commands;

namespace HRMS.Application.Features.Candidaties.Validators
{
    public class CreateCandidateValidator
    {
        public static string Validate(CreateCandidateCommand command)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(command.ResumePath))
            {
                errors.Add("Özgeçmiş yolunuz boş olamaz.");
            }

            return string.Join(" | ", errors);
        }
    }
}
