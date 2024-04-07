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

		public async Task<IEnumerable<FavoriteVehicle>> GetFavoriteVehiclesAsync(int userId)
		{
            var vehicles = _dataContext.FavoriteVehicles.Where(x=>x.UserId==userId);
            return await vehicles.ToListAsync();
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
