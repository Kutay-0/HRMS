using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.DTOs;
using HRMS.Application.Features.Companies.Queries;
using HRMS.Application.Repositories;
using HRMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.Features.Companies.Handlers
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, List<CompanyDto>>
    {
        private readonly IReadRepository<Company> _companyReadRepository;

        public GetAllCompaniesQueryHandler(IReadRepository<Company> companyReadRepository)
        {
            _companyReadRepository = companyReadRepository;
        }

        public async Task<List<CompanyDto>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = await _companyReadRepository.GetAll().Select(c => new CompanyDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToListAsync(cancellationToken);

            return companies;
        }
    }
}
