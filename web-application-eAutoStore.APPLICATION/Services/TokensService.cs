using Microsoft.AspNetCore.Http;
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
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using web_application_eAutoStore.DOMAIN.Models;
using web_application_eAutoStore.Interfaces.Repositories;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.APPLICATION.Services
{
    public class TokensService : ITokensService
    {
        private readonly TokensOptions _tokensOptions;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public TokensService(IOptions<TokensOptions> options, 
            IRefreshTokenRepository refreshTokenRepository,
			IHttpContextAccessor httpContextAccessor)
        {
            _tokensOptions = options.Value;
            _refreshTokenRepository = refreshTokenRepository;
			_httpContextAccessor = httpContextAccessor;

        }

		public void AddTokensToCookie(string jwt, string rt)
		{
			var jwtCookieOptions = new CookieOptions { Expires = DateTime.UtcNow.AddHours(_tokensOptions.ExpiresJwtHours) };
			var rtCookieOptions = new CookieOptions { Expires = DateTime.UtcNow.AddHours(_tokensOptions.ExpiresRtHours) };

			_httpContextAccessor.HttpContext.Response.Cookies.Append("jwt", jwt, jwtCookieOptions);
			_httpContextAccessor.HttpContext.Response.Cookies.Append("rt", rt, rtCookieOptions);
		}

		public string GenerateJWToken(UserDto user)
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

        public async Task<Guid> GenerateRefreshTokenAsync(UserDto user, string ipAddres)
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

		public bool GetTokensFromCookie(out string? jwt, out string? rt)
		{
			var isContaintJwt = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("jwt", out string? jwToken);
			var isContaintRt = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("rt", out string? refreshToken);

            jwt = jwToken;
            rt = refreshToken;

            if (!isContaintJwt || !isContaintRt)
                return false;

            return true;
        }

		public int? GetUserId()
		{
            string? jwtToken = null;
			var isContaintJwt = _httpContextAccessor?.HttpContext?.Request.Cookies.TryGetValue("jwt", out jwtToken);

			if (isContaintJwt != true)
                return null;

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.ReadJwtToken(jwtToken);
			bool result = int.TryParse(token.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value, out int userId);

            if (result == false)
                return null;

            return userId;
		}

		public async Task<bool> IsRefreshTokenValid(string refreshToken)
		{
			var refreshTokenModel = await _refreshTokenRepository.GetTokenModelAsync(refreshToken);

            if (refreshTokenModel == null)
                return false;

            if (refreshTokenModel.ExpiringAt.ToUniversalTime() > DateTime.UtcNow)
                return false;

			return true;
		}
	}
}
