using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Features.Candidaties.Commands;
using HRMS.Application.Repositories;
using HRMS.Domain.Entities;
using MediatR;

namespace HRMS.Application.Features.Candidaties.Handlers
{
    public class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, string>
    {
        private readonly IWriteRepository<Candidate> _candidateWriteRepository;
        private readonly IReadRepository<Candidate> _candidateReadRepository;

        public DeleteCandidateCommandHandler(IWriteRepository<Candidate> candidateWriteRepository, IReadRepository<Candidate> candidateReadRepository)
        {
            _candidateWriteRepository = candidateWriteRepository;
            _candidateReadRepository = candidateReadRepository;
        }

        public async Task<string> Handle(DeleteCandidateCommand command, CancellationToken cancellationToken)
        {
            var candidate = _candidateReadRepository.GetSingleAsync(c => c.ApplicationUserId == command.ApplicationUserId).Result;
            if (candidate == null)
            {
                throw new Exception("Aday bulunamadı.");
            }

            var result = await _candidateWriteRepository.Remove(candidate.Id);
            if (!result)
            {
                throw new Exception("Aday silinirken bir hata meydana geldi.");
            }
            await _candidateWriteRepository.SaveChangesAsync();

            return "Aday başarıyla silindi.";
        }
    }
}
