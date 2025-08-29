using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HRMS.Application.Features.CompanyRequests.Commands
{
    public class DeleteCompanyRequestCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
