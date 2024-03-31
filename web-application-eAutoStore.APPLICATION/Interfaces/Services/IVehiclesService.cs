using Microsoft.AspNetCore.Http;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Interfaces.Services
{
    public interface IVehiclesService
    {
		Task<IEnumerable<Vehicle>?> GetWithFiltersAsync(VehicleFiltersRequest vehicleFilters);
	}
}
