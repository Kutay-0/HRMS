using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HRMS.Domain.Entities
{
    //Tablomuz
    public class ApplicationUser : IdentityUser
    {
        //Sütunları
        public string FullName { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public ICollection<JobPosting> JobPostings { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
