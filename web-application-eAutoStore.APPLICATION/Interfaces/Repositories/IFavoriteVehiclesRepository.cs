using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Interfaces.Repositories
{
    public interface IFavoriteVehiclesRepository
    {
        Task<bool> SaveFavoriteVehicleAsync(FavoriteVehicle favoriteVehicle);
        Task<IEnumerable<FavoriteVehicle>> GetFavoriteVehiclesAsync(int userId);
        Task<bool> IsAlreadySavedAsync(FavoriteVehicle favoriteVehicle);
        Task<bool> DeleteFavoriteVehicleAsync(FavoriteVehicle favoriteVehicle);
		Task<bool> DeleteFavoriteVehiclesAsync(int vehicleId);
		Task<bool> IsExist(int favoriteVehicelId);
	}
}
