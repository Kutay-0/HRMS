using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.DTOs;
using HRMS.Application.Features.JobPostings.Queries;
using HRMS.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using HRMS.Domain.Entities;

namespace HRMS.Application.Features.JobPostings.Handlers
{
    public class GetJobPostingsByCompanyIdQueryHandler : IRequestHandler<GetJobPostingsByCompanyIdQuery , List<JobPostingDto>>
    {
        private readonly IReadRepository<Company> _companyReadRepository;

        public GetJobPostingsByCompanyIdQueryHandler(IReadRepository<Company> companyReadRepository)
        {
            _companyReadRepository = companyReadRepository;
        }

        public async Task<List<JobPostingDto>> Handle(GetJobPostingsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyReadRepository
                .GetAll().Include(c => c.JobPostings).FirstOrDefaultAsync(c => c.Id == request.CompanyId, cancellationToken);

            if (company == null)
                throw new Exception("Şirket Bulunamadı.");

            var postings = company.JobPostings.Select(p => new JobPostingDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                CompanyName = company.Name,
            }).ToList();

            return postings;
        }
    }
}
