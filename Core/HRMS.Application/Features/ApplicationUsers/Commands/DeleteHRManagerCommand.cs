using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HRMS.Application.Features.ApplicationUsers.Commands
{
    public class DeleteHRManagerCommand : IRequest<string>
    {
        public string Id { get; set; }
    }
}
