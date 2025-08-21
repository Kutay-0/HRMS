using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs
{
    public record LoginRequestDto(string Email, string Password);
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
