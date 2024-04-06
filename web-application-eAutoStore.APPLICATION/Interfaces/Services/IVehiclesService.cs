using Microsoft.AspNetCore.Http;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using web_application_eAutoStore.DOMAIN.DTOs.Vehicles;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Interfaces.Services
{
    public interface IVehiclesService
    {
		Task<IEnumerable<VehicleDto>?> GetWithFiltersAsync(VehicleFiltersRequest vehicleFilters);
		Task<int> GetQuantityAsync(VehicleFiltersRequest vehicleFilters);
		Task<VehicleDto?> GetVehicleAsync(int vehicleId);
		Task<string?> GetOwnerEmailAsync(int vehicleId);
		Task<bool> AddVehicleAsync(VehicleAddRequest vehicleAddRequest, string? imagePath, int ownerId);
		Task<string> SaveImageAsync(IFormFile image);
		Task<IEnumerable<VehicleDto>?> GetNewVehiclesAsync(int count);
	}
}
