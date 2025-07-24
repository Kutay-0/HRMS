using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using HRMS.Application.DTOs;

namespace HRMS.Application.Features.JobPostings.Queries
{
    //Filtrelemek istediklerimizi getiriyoruz
    public class GetJobPostingByFilterQuery : IRequest<List<JobPostingDto>>
    {
        public bool? IsActive { get; set; }
        public string? TitleKeyword { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public DateTime? UpdatedAfter { get; set; }
        public DateTime? UpdatedBefore { get; set; }
    }
}
