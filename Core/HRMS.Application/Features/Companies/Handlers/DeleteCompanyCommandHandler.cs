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
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, int>
    {
        private readonly IWriteRepository<Company> _companyWriteRepository;
        public DeleteCompanyCommandHandler(IWriteRepository<Company> companyWriteRepository)
        {
            _companyWriteRepository = companyWriteRepository;
        }

        public async Task<int> Handle(DeleteCompanyCommand command, CancellationToken cancellationToken)
        {
            await _companyWriteRepository.Remove(command.Id);
            await _companyWriteRepository.SaveChangesAsync();

            return command.Id;
        }
    }
}
