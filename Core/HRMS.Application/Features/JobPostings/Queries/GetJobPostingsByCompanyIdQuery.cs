using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.DTOs;
using MediatR;

namespace HRMS.Application.Features.JobPostings.Queries
{
    public class GetJobPostingsByCompanyIdQuery : IRequest<List<JobPostingDto>>
    {
        public int CompanyId { get; set; }
    }

}
