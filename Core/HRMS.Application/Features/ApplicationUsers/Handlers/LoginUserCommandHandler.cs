using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Abstractions.Token;
using HRMS.Application.DTOs;
using HRMS.Application.Features.ApplicationUsers.Commands;
using HRMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;

namespace HRMS.Application.Features.ApplicationUsers.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponseDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IConfiguration _configuration;

        public LoginUserCommandHandler
            (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenHandler tokenHandler,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("E-posta veya şifre hatalı.");
            }

            var signIn = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signIn.Succeeded)
            {
                throw new Exception("E-posta veya şifre hatalı.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email ?? string.Empty),
                new(ClaimTypes.Name, user.FullName ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(roles.Select(role => new Claim("role", role)));

            var minutes = int.TryParse(_configuration["Token:AccessTokenExpiration"], out var tokenExpiration) ? tokenExpiration : 60;
            var token = _tokenHandler.CreateAccessToken(minutes, claims);

            return new LoginResponseDto
            {
                AccessToken = token.AccessToken,
                Expiration = token.Expiration,
                UserId = user.Id,
                Email = user.Email,
            };
        }
    }
}
