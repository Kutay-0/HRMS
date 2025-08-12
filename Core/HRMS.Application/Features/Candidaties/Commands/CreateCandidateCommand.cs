using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HRMS.Application.Features.Candidaties.Commands
{
    public class CreateCandidateCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string ResumePath { get; set; }
        public int CompanyId { get; set; }
    }
}
