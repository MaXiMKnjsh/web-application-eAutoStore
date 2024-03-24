using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task<bool> AddAsync(User user);
        Task<bool> SaveAsync();
        Task<bool> IsExistAsync(string email);
    }
}
