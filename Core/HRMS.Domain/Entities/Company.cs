using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities
{
    //Tablomuz
    public class Company
    {
        //Sütunları
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<ApplicationUser> Employees { get; set; }
        public ICollection<JobPosting> JobPostings { get; set; }
        public ICollection<Candidate> Candidates { get; set; }
    }
}
