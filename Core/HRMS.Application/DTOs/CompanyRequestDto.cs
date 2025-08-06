using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs
{
    public class CompanyRequestDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string AboutCompany { get; set; } = string.Empty;
        public string EvidenceDocumentUrl { get; set; } = string.Empty;
        public string RequestedById { get; set; } = string.Empty;
        public string Status { get; set; } = "Beklemede";
        public DateTime RequestedAt { get; set; }
    }
}
