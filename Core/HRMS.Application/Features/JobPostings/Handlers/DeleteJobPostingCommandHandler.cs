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
    public class DeleteJobPostingCommandHandler : IRequestHandler<DeleteJobPostingCommand, int>
    {
        private readonly IWriteRepository<JobPosting> _jobPostingWriteRepository;
        public DeleteJobPostingCommandHandler(IWriteRepository<JobPosting> jobPostingWriteRepository)
        {
            _jobPostingWriteRepository = jobPostingWriteRepository;
        }
        public async Task<int> Handle(DeleteJobPostingCommand request, CancellationToken cancellationToken)
        {
            await _jobPostingWriteRepository.Remove(request.JobPostingId);
            await _jobPostingWriteRepository.SaveChangesAsync();

            return request.JobPostingId;
        }
    }
}
