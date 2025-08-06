using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Persistence.Context
{
    //Kod kısmında veri tabanı modellemek istiyorsak, Context nesnesine ihtiyacımız var.
    public class HRMSDbContext : IdentityDbContext<ApplicationUser>
    {
        //İleride sebebini anlayacağız.
        public HRMSDbContext(DbContextOptions options) : base(options)
        { }
        
        //Veri tabanında "ApplicationUser" türünde, "ApplicationUsers" adında bir tablo olacağını bildirdik
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }
        public DbSet<CompanyRequest> CompanyRequests { get; set; }
    }
}
