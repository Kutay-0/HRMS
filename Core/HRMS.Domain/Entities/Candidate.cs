using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities
{
    public class Candidate
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string ResumePath { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<JobPosting> JobPostings { get; set; }
    }
}
