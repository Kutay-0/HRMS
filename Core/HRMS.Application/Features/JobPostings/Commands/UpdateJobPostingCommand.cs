using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HRMS.Application.Features.JobPostings.Commands
{
    public class UpdateJobPostingCommand : IRequest<int>
    {
        public int JobPostingId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }
}
