using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.APPLICATION.Interfaces.Auth;
using web_application_eAutoStore.APPLICATION.Interfaces.Repositories;
using web_application_eAutoStore.DOMAIN.Models;
using web_application_eAutoStore.Interfaces.Repositories;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.APPLICATION.Services
{
    public class TokensService : ITokensService
    {
        private readonly TokensOptions _tokensOptions;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public TokensService(IOptions<TokensOptions> options, IRefreshTokenRepository refreshTokenRepository)
        {
            _tokensOptions = options.Value;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<string> GenerateJWTokenAsync(User user)
        {
            Claim[] claims = {
                new Claim("userId", user.Id.ToString()),
                new Claim("userRole", user.Role.ToString()),
                new Claim("userEmail", user.Email.ToString()),
            };

            var SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokensOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: SigningCredentials,
                expires: DateTime.UtcNow.AddHours(_tokensOptions.ExpiresJwtHours));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public async Task<Guid> GenerateRefreshTokenAsync(User user, string ipAddres)
        {
            var refreshToken = new RefreshToken()
            {
                GeneratedAt = DateTime.UtcNow,
                ExpiringAt = DateTime.UtcNow.AddHours(_tokensOptions.ExpiresRtHours),
                AssociatedDeviceName = ipAddres,
                UserId = user.Id
            };

            var guid = await _refreshTokenRepository.AddAsync(refreshToken);

            return guid;
        }
    }
}
