using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HRMS.Application.Features.JobPostings.Commands
{
    public class DeleteJobPostingCommand : IRequest<bool>
    {
        public int JobPostingId { get; set; }
    }
}
