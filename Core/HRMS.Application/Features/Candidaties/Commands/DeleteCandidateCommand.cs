using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HRMS.Application.Features.Candidaties.Commands
{
    public class DeleteCandidateCommand : IRequest<string>
    {
        public int JobPostingId { get; set; }
        public string ApplicationUserId { get; set; }
        public int CompanyId { get; set; }
    }
   
}
