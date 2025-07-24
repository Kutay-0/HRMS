using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.DTOs;
using HRMS.Application.Features.JobPostings.Queries;
using HRMS.Application.Repositories;
using HRMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.Features.JobPostings.Handlers
{
    public class GetAllJobPostingQueryHandler : IRequestHandler<GetAllJobPostingQuery, List<JobPostingDto>>
    {
        private readonly IReadRepository<JobPosting> _jobPostingReadRepository;
        public GetAllJobPostingQueryHandler(IReadRepository<JobPosting> jobPostingReadRepository)
        {
            _jobPostingReadRepository = jobPostingReadRepository;
        }
        public async Task<List<JobPostingDto>> Handle(GetAllJobPostingQuery request, CancellationToken cancellationToken)
        {

            var postings = await _jobPostingReadRepository.GetAll().Select(p => new JobPostingDto
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            IsActive = p.IsActive,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        })
        .ToListAsync(cancellationToken);

            return postings;
        }
    }
}
