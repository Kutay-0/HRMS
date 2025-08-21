using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs
{
    public class CandidateDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ResumePath { get; set; }
        public string JobPostingTitle { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
