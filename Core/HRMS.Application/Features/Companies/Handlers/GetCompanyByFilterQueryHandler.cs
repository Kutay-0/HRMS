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
    public class GetCompanyByFilterQueryHandler : IRequestHandler<GetCompanyByFilterQuery, List<CompanyDto>>
    {
        private readonly IReadRepository<Company> _companyReadRepository;

        public GetCompanyByFilterQueryHandler(IReadRepository<Company> companyReadRepository)
        {
            _companyReadRepository = companyReadRepository;
        }

        public async Task<List<CompanyDto>> Handle(GetCompanyByFilterQuery request, CancellationToken cancellationToken)
        {
            var query = _companyReadRepository.GetAll();

            if (!string.IsNullOrEmpty(request.NameKeyword))
            {
                query = query.Where(c => c.Name.ToLower().Contains(request.NameKeyword.ToLower()));
            }

            if (request.CreatedAfter.HasValue)
            {
                var utcDate = DateTime.SpecifyKind(request.CreatedAfter.Value, DateTimeKind.Utc);
                query = query.Where(c => c.CreatedAt > utcDate);
            }

            if (request.CreatedBefore.HasValue)
            {
                var utcDate = DateTime.SpecifyKind(request.CreatedBefore.Value, DateTimeKind.Utc);
                query = query.Where(c => c.CreatedAt < utcDate);
            }

            if (request.UpdatedAfter.HasValue)
            {
                var utcDate = DateTime.SpecifyKind(request.UpdatedAfter.Value, DateTimeKind.Utc);
                query = query.Where(c => c.UpdatedAt > utcDate);
            }

            if (request.UpdatedBefore.HasValue)
            {
                var utcDate = DateTime.SpecifyKind(request.UpdatedBefore.Value, DateTimeKind.Utc);
                query = query.Where(c => c.UpdatedAt < utcDate);
            }

            var result = await query
                .Select(c => new CompanyDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
