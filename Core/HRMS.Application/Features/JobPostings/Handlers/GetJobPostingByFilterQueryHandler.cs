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
                query = query.Where(j => j.Title.ToLower().Contains(request.TitleKeyword.ToLower()));

            }

            if (request.CreatedAfter.HasValue)
            {
                var utcDate = DateTime.SpecifyKind(request.CreatedAfter.Value, DateTimeKind.Utc);
                query = query.Where(j => j.CreatedAt > utcDate);
            }

            if(request.CreatedBefore.HasValue)
            {
                var utcDate = DateTime.SpecifyKind(request.CreatedBefore.Value, DateTimeKind.Utc);
                query = query.Where(j => j.CreatedAt < utcDate);
            }

            if(request.UpdatedAfter.HasValue)
            {
                var utcDate = DateTime.SpecifyKind(request.UpdatedAfter.Value, DateTimeKind.Utc);
                query = query.Where(j => j.UpdatedAt > utcDate);
            }

            if(request.UpdatedBefore.HasValue)
            {
                var utcDate = DateTime.SpecifyKind(request.UpdatedBefore.Value, DateTimeKind.Utc);
                query = query.Where(j => j.UpdatedAt < utcDate);
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
