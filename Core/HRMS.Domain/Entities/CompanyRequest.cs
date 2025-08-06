using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities
{
    public class CompanyRequest
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string AboutCompany { get; set; }
        public string EvidenceDocumentUrl { get; set; }
        public string RequestedById { get; set; }
        public string Status { get; set; } = "Beklemede";
        public DateTime RequestedAt { get; set; }
    }
}
