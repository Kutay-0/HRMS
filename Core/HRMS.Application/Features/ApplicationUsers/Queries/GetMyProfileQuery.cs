using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.DTOs;
using MediatR;

namespace HRMS.Application.Features.ApplicationUsers.Queries
{
    public class GetMyProfileQuery : IRequest<UserDto>
    {
        public string Id { get; set; }
    }
}
