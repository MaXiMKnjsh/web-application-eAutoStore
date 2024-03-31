using Microsoft.AspNetCore.Http;
using web_application_eAutoStore.APPLICATION.Interfaces.Auth;
using web_application_eAutoStore.APPLICATION.Interfaces.Repositories;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using web_application_eAutoStore.Enumerations;
using web_application_eAutoStore.Interfaces.Repositories;
using web_application_eAutoStore.Interfaces.Services;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Services
{
    public class VehiclesService : IVehiclesService
	{
		private readonly IVehiclesRepository _vehiclesRepository;

		public VehiclesService(IVehiclesRepository vehiclesRepository)
        {
			_vehiclesRepository = vehiclesRepository;
        }

		public async Task<IEnumerable<Vehicle>?> GetWithFiltersAsync(VehicleFiltersRequest vehicleFilters) 
        {
			var vehicles = await _vehiclesRepository.GetWithFiltersAsync(vehicleFilters);
			return vehicles;
		}
	}
}
