using Microsoft.AspNetCore.Http;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using web_application_eAutoStore.DOMAIN.DTOs.Vehicles;
using web_application_eAutoStore.Interfaces.Repositories;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Interfaces.Services
{
    public interface IVehiclesService
    {
		Task<IEnumerable<VehicleDto>?> GetWithFiltersAsync(VehicleFiltersRequest vehicleFilters, int portionSize);
		Task<int> GetQuantityAsync(VehicleFiltersRequest vehicleFilters);
		Task<VehicleDto?> GetVehicleAsync(int vehicleId);
		Task<string?> GetOwnerEmailAsync(int vehicleId);
		Task<bool> AddVehicleAsync(VehicleAddRequest vehicleAddRequest, string? imagePath, int ownerId);
		Task<string> SaveImageAsync(IFormFile image);
		Task<IEnumerable<VehicleDto>?> GetNewVehiclesAsync(int count);
		Task<bool> IsAlreadySavedAsync(int vehicleId);
		Task<bool> DeleteVehicleAsync(int vehicleId);
		Task<VehicleEstimates> GetMarketEstimates(int vehicleId);
		Task<bool> DeleteVehicleWithReasonAsync(DeleteVehicleRequest request);

    }
}
