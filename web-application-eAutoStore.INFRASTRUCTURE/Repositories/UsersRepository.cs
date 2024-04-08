using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using web_application_eAutoStore.Data;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using web_application_eAutoStore.DOMAIN.DTOs.Vehicles;
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

		public async Task<IEnumerable<Vehicle>?> GetAdsByIdAsync(IEnumerable<int> vehsIds)
		{
			var vehicles = _dataContext.Vehicles.Where(x => vehsIds.Contains(x.Id));
			return await vehicles.ToListAsync();
		}

		public async Task<User> GetByEmailAsync(string email)
		{
			return await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email);
		}

		public async Task<IEnumerable<Vehicle>?> GetUserAdvertisementsAsync(int id)
		{
			var vehicles = _dataContext.Vehicles.Where(x => x.OwnerId == id);
			return await vehicles.ToListAsync();
		}

		public async Task<User?> GetUserByIdAsync(int id)
		{
			var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
			return user;
		}

		public async Task<User> GetUserByRefreshToken(string refreshToken)
		{
			var refreshTokenModel = await _dataContext.RefreshTokens.FirstOrDefaultAsync(x => x.Guid.ToString() == refreshToken);
			return await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == refreshTokenModel.UserId);
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

		public async Task<bool> UpdateUserInfoAsync(UpdateUserRequest updateUserRequest, int userId, string? hashedPassword)
		{
			var user = _dataContext.Users.FirstOrDefault(x=>x.Id==userId);

			if (user==null)
				return false;

			if (updateUserRequest.Name!=null)
				user.Name= updateUserRequest.Name;

			if (updateUserRequest.Surname != null)
				user.Surname= updateUserRequest.Surname;

			if (hashedPassword!=null)
				user.HashedPassword = hashedPassword;

			return await SaveAsync();
		}
	}
}
