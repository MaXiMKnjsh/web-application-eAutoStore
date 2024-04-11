using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Interfaces.Repositories
{
    public interface IFavoriteVehiclesRepository
    {
        Task<bool> SaveFavoriteVehicleAsync(int userId, int favoriteVehicleId);
        Task<IEnumerable<FavoriteVehicle>> GetFavoriteVehiclesAsync(int userId);
        Task<bool> IsAlreadySavedAsync(int userId, int favoriteVehicleId);
        Task<bool> DeleteFavoriteVehicleAsync(int vehicleId, int userId);
		Task<bool> DeleteFavoriteVehiclesAsync(int vehicleId);
		Task<bool> IsExist(int favoriteVehicelId);
	}
}
