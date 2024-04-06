using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.APPLICATION.Interfaces.Auth
{
    public interface ITokensService
    {
        Task<Guid> GenerateRefreshTokenAsync(UserDto user, string ipAddres);
        string GenerateJWToken(UserDto user);
        Task<bool> IsRefreshTokenValid(string refreshToken);
        int? GetUserId();
        void AddTokensToCookie(string jwt, string rt);
        bool GetTokensFromCookie(out string? jwt,out string? rt);
	}
    
}
