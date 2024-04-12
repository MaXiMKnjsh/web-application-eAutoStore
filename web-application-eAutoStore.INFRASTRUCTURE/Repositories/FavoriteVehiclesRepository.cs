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

		public async Task<bool> DeleteFavoriteVehicleAsync(FavoriteVehicle favoriteVehicle)
		{
			if (favoriteVehicle==null)
				return false;

			var vehicleToDelete = await _dataContext.FavoriteVehicles.FirstOrDefaultAsync(x => x.VehicleId == favoriteVehicle.VehicleId && x.UserId == favoriteVehicle.UserId);

			if (vehicleToDelete != null)
				_dataContext.FavoriteVehicles.Remove(vehicleToDelete);

			return await SaveAsync();
		}

		public async Task<bool> DeleteFavoriteVehiclesAsync(int vehicleId)
		{
			var vehiclesToDelete = await _dataContext.FavoriteVehicles.Where(x => x.VehicleId == vehicleId).ToListAsync();

			if (vehiclesToDelete != null)
				_dataContext.FavoriteVehicles.RemoveRange(vehiclesToDelete);

			return await SaveAsync();
		}

		public async Task<IEnumerable<FavoriteVehicle>> GetFavoriteVehiclesAsync(int userId)
		{
			var vehicles = _dataContext.FavoriteVehicles.Where(x => x.UserId == userId);
			return await vehicles.ToListAsync();
		}

		public async Task<bool> IsAlreadySavedAsync(FavoriteVehicle favoriteVehicle)
		{
			if (favoriteVehicle == null)
				return false;

				var favVehicle = await _dataContext.FavoriteVehicles.FirstOrDefaultAsync(x => x.UserId == favoriteVehicle.UserId && x.VehicleId == favoriteVehicle.VehicleId);

			if (favVehicle != null)
				return true;

			return false;
		}

		public async Task<bool> IsExist(int favoriteVehicelId)
		{
			var favVehicle = await _dataContext.FavoriteVehicles.FirstOrDefaultAsync(x=>x.VehicleId == favoriteVehicelId);

			if (favVehicle != null)
				return true;

			return false;
		}

		public async Task<bool> SaveAsync()
		{
			int savedCount = await _dataContext.SaveChangesAsync();
			return savedCount > 0 ? true : false;
		}

		public async Task<bool> SaveFavoriteVehicleAsync(FavoriteVehicle favoriteVehicle)
		{
			if (favoriteVehicle!=null)
			await _dataContext.FavoriteVehicles.AddAsync(favoriteVehicle);
			return await SaveAsync();
		}
	}
}
