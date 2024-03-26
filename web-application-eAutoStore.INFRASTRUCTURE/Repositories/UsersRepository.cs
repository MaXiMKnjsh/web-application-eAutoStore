using Microsoft.EntityFrameworkCore;
using web_application_eAutoStore.Data;
using web_application_eAutoStore.Interfaces.Repositories;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _dataContext;
        public UsersRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> AddAsync(User user)
        {
            await _dataContext.Users.AddAsync(user);
            return await SaveAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x=>x.Email==email);
        }

        public async Task<bool> IsExistAsync(string email)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user != null ? true : false;
        }

        public async Task<bool> SaveAsync()
        {
            int savedCount = await _dataContext.SaveChangesAsync();
            return savedCount > 0 ? true : false;
        }
    }
}
