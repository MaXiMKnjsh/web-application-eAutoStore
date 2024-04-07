using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_application_eAutoStore.DOMAIN.DTOs.FavoriteVehicles;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using web_application_eAutoStore.DOMAIN.DTOs.Vehicles;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Interfaces.Services
{
    public interface IFavoriteVehiclesService
	{
		Task<bool> SaveFavoriteVehicleAsync(int userId, int favoriteVehicleId);
		Task<IEnumerable<FavVehicleDto>?> GetFavoriteVehiclesAsync(int userId);
	}
}
