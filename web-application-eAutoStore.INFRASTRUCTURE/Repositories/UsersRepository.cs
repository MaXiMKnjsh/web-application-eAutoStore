using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
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

		public async Task<IEnumerable<Vehicle>?> GetUserAdvertisementsAsync(int id)
		{
			var vehicles = _dataContext.Vehicles.Where(x=>x.Id==id);
            return await vehicles.ToListAsync();
		}

		public async Task<User?> GetUserByIdAsync(int id)
		{
            var user = await _dataContext.Users.FirstOrDefaultAsync(x=>x.Id==id);
            return user;
		}

		public async Task<User> GetUserByRefreshToken(string refreshToken)
		{
            var refreshTokenModel = await _dataContext.RefreshTokens.FirstOrDefaultAsync(x=>x.Guid.ToString()==refreshToken);
            return await _dataContext.Users.FirstOrDefaultAsync(x=>x.Id==refreshTokenModel.UserId);
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
