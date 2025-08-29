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
using HRMS.Application.Abstractions.User;

namespace HRMS.Application.Features.JobPostings.Handlers
{
    public class GetJobPostingsByCompanyIdQueryHandler : IRequestHandler<GetJobPostingsByCompanyIdQuery , List<JobPostingDto>>
    {
        private readonly IReadRepository<ApplicationUser> _userReadRepository;
        private readonly IReadRepository<Company> _companyReadRepository;
        private readonly IReadRepository<JobPosting> _jobPostingReadRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetJobPostingsByCompanyIdQueryHandler(IReadRepository<ApplicationUser> userReadRepository, IReadRepository<Company> companyReadRepository, IReadRepository<JobPosting> jobPostingReadRepository, ICurrentUserService currentUserService)
        {
            _userReadRepository = userReadRepository;
            _companyReadRepository = companyReadRepository;
            _jobPostingReadRepository = jobPostingReadRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<JobPostingDto>> Handle(GetJobPostingsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var user = await _userReadRepository.GetSingleAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            var postings = await _jobPostingReadRepository.Table
                    .AsNoTracking()
                    .Where(p => p.CompanyId == user.CompanyId)
                    .Include(p => p.Company)
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

            return postings;
        }
    }
}
