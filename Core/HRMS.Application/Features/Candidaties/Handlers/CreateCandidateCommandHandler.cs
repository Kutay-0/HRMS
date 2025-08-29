using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Features.Candidaties.Commands;
using HRMS.Application.Features.Candidaties.Validators;
using HRMS.Application.Repositories;
using HRMS.Domain.Entities;
using MediatR;

namespace HRMS.Application.Features.Candidaties.Handlers
{
    public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, string>
    {
        private readonly IWriteRepository<Candidate> _candidateWriteRepository;
        private readonly IReadRepository<Candidate> _candidateReadRepository;
        private readonly IReadRepository<ApplicationUser> _userReadRepository;

        public CreateCandidateCommandHandler(
            IWriteRepository<Candidate> candidateWriteRepository,
            IReadRepository<Candidate> candidateReadRepository,
            IReadRepository<ApplicationUser> userReadRepository)
        {
            _candidateWriteRepository = candidateWriteRepository;
            _candidateReadRepository = candidateReadRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task<string> Handle(CreateCandidateCommand command, CancellationToken cancellationToken)
        {
            var alreadyApplied = await _candidateReadRepository.GetSingleAsync(c => c.ApplicationUserId == command.UserId && c.JobPostingId == command.JobPostingId);
            if(alreadyApplied != null)
            {
                throw new Exception("Bu ilana daha önce başvurdunuz.");
            }

            var validationMessage = CreateCandidateValidator.Validate(command);
            if (!string.IsNullOrEmpty(validationMessage))
            {
                throw new Exception(validationMessage);
            }

            var candidate = new Candidate
            {
                ApplicationUserId = command.UserId,
                JobPostingId = command.JobPostingId,
                ApplicationStatus = "Başvuru Alındı",
                ResumePath = command.ResumePath,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _candidateWriteRepository.AddAsync(candidate);
            if (!result)
            {
                throw new Exception("Aday oluşturulamadı.");
            }
            await _candidateWriteRepository.SaveChangesAsync();
            return "Aday başarıyla oluşturuldu.";
        }
    }
}
