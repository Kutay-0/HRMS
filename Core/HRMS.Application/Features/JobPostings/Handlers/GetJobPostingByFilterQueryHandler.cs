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
    public class GetJobPostingByFilterQueryHandler : IRequestHandler<GetJobPostingByFilterQuery, List<JobPostingDto>>
    {
        private readonly IReadRepository<JobPosting> _jobPostingReadRepository;

        public GetJobPostingByFilterQueryHandler(IReadRepository<JobPosting> jobPostingReadRepository)
        {
            _jobPostingReadRepository = jobPostingReadRepository;
        }
        public async Task<List<JobPostingDto>> Handle(GetJobPostingByFilterQuery request, CancellationToken cancellationToken)
        {
            var query = _jobPostingReadRepository.GetAll();

            if(request.IsActive.HasValue)
            {
                query = query.Where(p => p.IsActive == request.IsActive.Value);
            }

            if(request.CompanyId.HasValue)
            {
                query = query.Where(p => p.CompanyId == request.CompanyId.Value);
            }

            if(!string.IsNullOrEmpty(request.TitleKeyword))
            {
                query = query.Where(p => p.Title.Contains(request.TitleKeyword, StringComparison.OrdinalIgnoreCase));
            }

            if(request.CreatedAfter.HasValue)
            {
                query = query.Where(p => p.CreatedAt >= request.CreatedAfter.Value);
            }

            if(request.CreatedBefore.HasValue)
            {
                query = query.Where(p => p.CreatedAt <= request.CreatedBefore.Value);
            }

            if(request.UpdatedAfter.HasValue)
            {
                query = query.Where(p => p.UpdatedAt >= request.UpdatedAfter.Value);
            }

            if(request.UpdatedBefore.HasValue)
            {
                query = query.Where(p => p.UpdatedAt <= request.UpdatedBefore.Value);
            }

            var result = await query
                .Select(p => new JobPostingDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    IsActive = p.IsActive,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
