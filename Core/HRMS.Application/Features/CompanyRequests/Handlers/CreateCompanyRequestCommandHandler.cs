using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Abstractions.User;
using HRMS.Application.Features.CompanyRequests.Commands;
using HRMS.Application.Repositories;
using HRMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HRMS.Application.Features.CompanyRequests.Handlers
{
    public class CreateCompanyRequestCommandHandler : IRequestHandler<CreateCompanyRequestCommand, int>
    {
        private readonly IWriteRepository<CompanyRequest> _companyRequestWriteRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IReadRepository<ApplicationUser> _userReadRepository;

        public CreateCompanyRequestCommandHandler(IWriteRepository<CompanyRequest> companyRequestWriteRepository, ICurrentUserService currentUserService, IReadRepository<ApplicationUser> userReadRepository)
        {
            _companyRequestWriteRepository = companyRequestWriteRepository;
            _currentUserService = currentUserService;
            _userReadRepository = userReadRepository;
        }

        public async Task<int> Handle(CreateCompanyRequestCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var user = await _userReadRepository.GetSingleAsync(u => u.Id == userId);

            if(user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            if (string.IsNullOrWhiteSpace(request.CompanyName))
            {
                throw new Exception("Şirket adı boş olamaz.");
            }

            if (string.IsNullOrWhiteSpace(request.EvidenceDocumentUrl))
            {
                throw new Exception("Delil belgesi URL'si boş olamaz.");
            }

            var companyRequest = new CompanyRequest
            {
                CompanyName = request.CompanyName.Trim(),
                AboutCompany = request.AboutCompany,
                EvidenceDocumentUrl = request.EvidenceDocumentUrl,
                Status = "Beklemede",
                RequestedById = userId,
                RequestedAt = DateTime.UtcNow
            };

            var result = await _companyRequestWriteRepository.AddAsync(companyRequest);
            
            if(!result)
            {
                throw new Exception("Şirket talebi oluşturulurken bir hata meydana geldi.");
            }

            var saveResult = await _companyRequestWriteRepository.SaveChangesAsync();

            return companyRequest.Id;
        }
    }
}
