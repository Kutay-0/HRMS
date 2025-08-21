using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Abstractions
{
    public interface ICurrentUser
    {
        string? UserId { get; }
        string? Email { get; }
        int? CompanyId { get; }
        string? FullName { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(string role);
    }
}
