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
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
