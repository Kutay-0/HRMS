using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HRMS.Application.Features.CompanyRequests.Commands
{
    public class CreateCompanyRequestCommand : IRequest<int>
    {
        public string CompanyName { get; set; }
        public string? AboutCompany { get; set; }
        public string EvidenceDocumentUrl { get; set; }

    }
}
