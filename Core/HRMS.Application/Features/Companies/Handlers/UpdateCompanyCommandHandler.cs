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
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, int>
    {
        private readonly IWriteRepository<Company> _companyWriteRepository;
        private readonly IReadRepository<Company> _companyReadRepository;

        public UpdateCompanyCommandHandler(IWriteRepository<Company> companyWriteRepository, IReadRepository<Company> companyReadRepository)
        {
            _companyWriteRepository = companyWriteRepository;
            _companyReadRepository = companyReadRepository;
        }

        public async Task<int> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyReadRepository.GetSingleAsync(c => c.Id == request.Id);
            if (company == null)
            {
                throw new Exception("İş yeri bulunamadı.");
            }

            company.Description = request.Description;
            company.UpdatedAt = DateTime.UtcNow;

            await _companyWriteRepository.UpdateAsync(company);
            await _companyWriteRepository.SaveChangesAsync();

            return company.Id;
        }
    }
}
