using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Abstractions.User;
using HRMS.Application.Features.JobPostings.Commands;
using HRMS.Application.Repositories;
using HRMS.Domain.Entities;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HRMS.Application.Features.JobPostings.Handlers
{
    public class CreateJobPostingCommandHandler : IRequestHandler<CreateJobPostingCommand, int>
    {
        private readonly IWriteRepository<JobPosting> _jobPostingWriteRepository;
        private readonly IReadRepository<ApplicationUser> _userReadRepository;
        private readonly ICurrentUserService _currentUserService;
        public CreateJobPostingCommandHandler(IWriteRepository<JobPosting> jobPostingWriteRepository, ICurrentUserService currentUserService, IReadRepository<ApplicationUser> userReadRepository)
        {
            _jobPostingWriteRepository = jobPostingWriteRepository;
            _userReadRepository = userReadRepository;
            _currentUserService = currentUserService;
        }
        public async Task<int> Handle(CreateJobPostingCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var user = await _userReadRepository.GetSingleAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            if (user.CompanyId == null)
                throw new Exception("HRManager olmayan bir kullanıcı ilan oluşturamaz.");

            var jobPosting = new JobPosting
            {
                Title = request.Title,
                Description = request.Description,
                CompanyId = user.CompanyId ?? 0,
                IsActive = true,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = user
            };
            await _jobPostingWriteRepository.AddAsync(jobPosting);
            await _jobPostingWriteRepository.SaveChangesAsync();

            return jobPosting.Id; 
        }
    }
}
