using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HRMS.Application.Features.Companies.Commands
{
    public class DeleteCompanyCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
