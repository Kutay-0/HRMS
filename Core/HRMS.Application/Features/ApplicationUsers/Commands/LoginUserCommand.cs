using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.DTOs;
using MediatR;

namespace HRMS.Application.Features.ApplicationUsers.Commands
{
    public class LoginUserCommand : IRequest<LoginResponseDto>
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
