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
        public string UserId { get; set; }
        public int JobPostingId { get; set; }
        public string ResumePath { get; set; }
    }
}
