using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using web_application_eAutoStore.DOMAIN.DTOs.Vehicles;
using web_application_eAutoStore.DOMAIN.Models;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.APPLICATION.Interfaces.Repositories
{
    public interface IVehiclesRepository
    {
        Task<IEnumerable<Vehicle>?> GetWithFiltersAsync(VehicleFiltersRequest vehicleFilters, int portionSize);
		Task<int> GetQuantityAsync(VehicleFiltersRequest vehicleFilters);
        Task<Vehicle?> GetVehicleAsync(int vehicleId);
        Task<string?> GetOwnerEmailAsync(int vehicleId);
        Task<bool> AddVehicleAsync(Vehicle newVehicle);
        Task<bool> SaveAsync();
        Task<IEnumerable<Vehicle>> GetNewVehiclesAsync(int count);
	}
}
