using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.DTOs;
using MediatR;

namespace HRMS.Application.Features.Candidaties.Queries
{
    public class GetCandidateByFilterQuery : IRequest<List<CandidateDto>>
    {
        public int JobPostingId { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public string? EmailKeyword { get; set; }
    }
}
