using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.DTOs;

namespace HRMS.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        TokenDto CreateAccessToken(int minute, IEnumerable<Claim> claims);
    }
}
