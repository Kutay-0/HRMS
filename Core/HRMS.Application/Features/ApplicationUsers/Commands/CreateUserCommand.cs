using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HRMS.Application.Features.ApplicationUsers.Commands
{
    //Burada IRequest<string> kullanmamızın sebebi IdentityUser sınıfında ID string olduğu için.
    public class CreateUserCommand : IRequest<string>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
