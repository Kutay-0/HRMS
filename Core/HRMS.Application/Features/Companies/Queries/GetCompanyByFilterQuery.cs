using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.DTOs;
using MediatR;

namespace HRMS.Application.Features.Companies.Queries
{
    public class GetCompanyByFilterQuery : IRequest<List<CompanyDto>>
    {
        public int Id { get; set; }
        public string? NameKeyword { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public DateTime? UpdatedAfter { get; set; }
        public DateTime? UpdatedBefore { get; set; }
    }
}
