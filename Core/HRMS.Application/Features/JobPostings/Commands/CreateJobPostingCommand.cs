using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HRMS.Application.Features.JobPostings.Commands
{
    //Amacımız kullanıcıdan ilan bilgilerini almak / IRequest<int> : int türünden değer döndürmek için
    public class CreateJobPostingCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
    }
}
