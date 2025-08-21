using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.DTOs;
using HRMS.Application.Features.Candidaties.Queries;
using HRMS.Application.Repositories;
using HRMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.Features.Candidaties.Handlers
{
    public class GetCandidateByFilterQueryHandler : IRequestHandler<GetCandidateByFilterQuery, List<CandidateDto>>
    {
        private IReadRepository<Candidate> _candidateReadRepository;

        public GetCandidateByFilterQueryHandler(IReadRepository<Candidate> candidateReadRepository)
        {
            _candidateReadRepository = candidateReadRepository;
        }

        public async Task<List<CandidateDto>> Handle(GetCandidateByFilterQuery request, CancellationToken cancellationToken)
        {
            var query = _candidateReadRepository.GetAll().Include(c => c.ApplicationUser).Where(c => c.JobPostingId == request.JobPostingId);

            if (request.CreatedAfter.HasValue)
            {
                var utc = DateTime.SpecifyKind(request.CreatedAfter.Value, DateTimeKind.Utc);
                query = query.Where(c => c.CreatedAt >= utc);
            }

            if (request.CreatedBefore.HasValue)
            {
                var utc = DateTime.SpecifyKind(request.CreatedBefore.Value, DateTimeKind.Utc);
                query = query.Where(c => c.CreatedAt <= utc);
            }

            if (!string.IsNullOrEmpty(request.EmailKeyword))
            {
                query = query.Where(c => c.ApplicationUser.Email.ToLower().Contains(request.EmailKeyword.ToLower()));
            }

            var result = await query.Select(c => new CandidateDto
            {
                FullName = c.ApplicationUser.FullName,
                Email = c.ApplicationUser.Email,
                PhoneNumber = c.ApplicationUser.PhoneNumber,
                ResumePath = c.ResumePath
            }).ToListAsync(cancellationToken);

            if(result == null)
            {
                throw new Exception("Hiç aday bulunamadı.");
            }

            return result;
        }
    }
}
