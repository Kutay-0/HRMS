using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Features.Companies.Commands;
using HRMS.Application.Repositories;
using HRMS.Domain.Entities;
using MediatR;

namespace HRMS.Application.Features.Companies.Handlers
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, int>
    {
        private readonly IWriteRepository<Company> _companyWriteRepository;
        private readonly IReadRepository<Company> _companyReadRepository;

        public CreateCompanyCommandHandler(IWriteRepository<Company> companyWriteRepository, IReadRepository<Company> companyReadRepository)
        {
            _companyWriteRepository = companyWriteRepository;
            _companyReadRepository = companyReadRepository;
        }
        public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var exists = await _companyReadRepository.GetSingleAsync(c => c.Name.ToLower() == request.Name.ToLower());

            if (exists != null)
                throw new Exception("Bu şirket zaten kayıtlı.");

            var company = new Company
            {
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _companyWriteRepository.AddAsync(company);
            await _companyWriteRepository.SaveChangesAsync();

            return company.Id; // Yeni oluşturulan şirketin Id'sini döndürür
        }
    }
}
