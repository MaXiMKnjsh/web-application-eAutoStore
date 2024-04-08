using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using web_application_eAutoStore.Data;
using web_application_eAutoStore.Interfaces.Repositories;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Repositories
{
	public class FavoriteVehiclesRepository : IFavoriteVehiclesRepository
	{
		private readonly DataContext _dataContext;
		public FavoriteVehiclesRepository(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async Task<bool> DeleteFavoriteVehicleAsync(int vehicleId, int userId)
		{
			var vehicleToDelete = await _dataContext.FavoriteVehicles.FirstOrDefaultAsync(x => x.VehicleId == vehicleId && x.UserId == userId);

			if (vehicleToDelete != null)
				_dataContext.FavoriteVehicles.Remove(vehicleToDelete);

			return await SaveAsync();
		}

		public async Task<IEnumerable<FavoriteVehicle>> GetFavoriteVehiclesAsync(int userId)
		{
			var vehicles = _dataContext.FavoriteVehicles.Where(x => x.UserId == userId);
			return await vehicles.ToListAsync();
		}

		public async Task<bool> IsAlreadySavedAsync(int userId, int favoriteVehicleId)
		{
			var favVehicle = await _dataContext.FavoriteVehicles.FirstOrDefaultAsync(x => x.UserId == userId && x.VehicleId == favoriteVehicleId);

			if (favVehicle != null)
				return true;

			return false;
		}

		public async Task<bool> SaveAsync()
		{
			int savedCount = await _dataContext.SaveChangesAsync();
			return savedCount > 0 ? true : false;
		}

		public async Task<bool> SaveFavoriteVehicleAsync(int userId, int favoriteVehicleId)
		{
			var newFavoriteVehicle = new FavoriteVehicle()
			{
				VehicleId = favoriteVehicleId,
				UserId = userId
			};

			await _dataContext.FavoriteVehicles.AddAsync(newFavoriteVehicle);
			return await SaveAsync();
		}
	}
}
