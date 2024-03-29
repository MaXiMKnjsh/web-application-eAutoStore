using Microsoft.AspNetCore.Http;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Interfaces.Services
{
    public interface IUsersService
    {
        Task<bool> RegisterAsync(string name, string email, string password);
        Task<bool> IsExistAsync(string email);
        Task<bool> LoginAsync(string email, string password);
        Task<User> GetUserByEmailAsync(string email);
    }
}
