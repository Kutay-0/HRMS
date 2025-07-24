using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.DTOs;
using MediatR;

namespace HRMS.Application.Features.JobPostings.Queries
{
    //İşlem yapmayacak sadece değer döndürecek o yüzden içi boş
    public class GetAllJobPostingQuery : IRequest<List<JobPostingDto>>
    {
    }
}
