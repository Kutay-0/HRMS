using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Features.JobPostings.Commands;
using HRMS.Application.Repositories;
using HRMS.Domain.Entities;
using MediatR;

namespace HRMS.Application.Features.JobPostings.Handlers
{
    public class CreateJobPostingCommandHandler : IRequestHandler<CreateJobPostingCommand, int>
    {
        private readonly IWriteRepository<JobPosting> _jobPostingWriteRepository;
        private readonly IReadRepository<ApplicationUser> _userReadRepository;
        public CreateJobPostingCommandHandler(IWriteRepository<JobPosting> jobPostingWriteRepository, IReadRepository<ApplicationUser> userReadRepository)
        {
            _jobPostingWriteRepository = jobPostingWriteRepository;
            _userReadRepository = userReadRepository;
        }
        public async Task<int> Handle(CreateJobPostingCommand request, CancellationToken cancellationToken)
        {
            var jobPosting = new JobPosting
            {
                Title = request.Title,
                Description = request.Description,
                CompanyId = request.CompanyId,
                IsActive = true,
                CreatedById = request.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = await _userReadRepository.GetSingleAsync(u => u.Id == request.UserId)
            };
            await _jobPostingWriteRepository.AddAsync(jobPosting);
            await _jobPostingWriteRepository.SaveChangesAsync();

            return jobPosting.Id; 
        }
    }
}
