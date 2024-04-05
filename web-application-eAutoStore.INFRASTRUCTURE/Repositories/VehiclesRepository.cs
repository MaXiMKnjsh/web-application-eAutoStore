using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using web_application_eAutoStore.APPLICATION.Interfaces.Repositories;
using web_application_eAutoStore.Data;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using web_application_eAutoStore.DOMAIN.DTOs.Vehicles;
using web_application_eAutoStore.DOMAIN.Models;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.INFRASTRUCTURE.Repositories
{
	public class VehiclesRepository : IVehiclesRepository
	{
		private readonly DataContext _dataContext;
		public VehiclesRepository(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async Task<int> GetQuantityAsync(VehicleFiltersRequest vehicleFilters)
		{
			var vehicles = ApplyFilters(vehicleFilters);

			if (vehicles == null)
				return 0;

			var totalQuantity = await vehicles.CountAsync();

			return totalQuantity;
		}
		private IQueryable<Vehicle>? ApplyFilters(VehicleFiltersRequest vehicleFilters)
		{
			IQueryable<Vehicle>? vehicles = null;

			if (vehicleFilters.Model != null)
				vehicles = _dataContext.Vehicles.Where(x => x.Model == vehicleFilters.Model);

			if (vehicleFilters.Brand != null)
				vehicles = _dataContext.Vehicles.Where(x => x.Brand == vehicleFilters.Brand);

			if (vehicleFilters.Mileage != null)
				vehicles = _dataContext.Vehicles.Where(x=>x.Mileage == vehicleFilters.Mileage);

			if (vehicleFilters.Type != null)
				vehicles = _dataContext.Vehicles.Where(x => x.Type == vehicleFilters.Type);

			if (vehicleFilters.Quality != null)
				vehicles = _dataContext.Vehicles.Where(x => x.Quality == vehicleFilters.Quality);

			if (vehicleFilters.Quality != null)
				vehicles = _dataContext.Vehicles.Where(x => x.Quality == vehicleFilters.Quality);

			if (vehicleFilters.PriceFrom != null)
				vehicles = _dataContext.Vehicles.Where(x => x.Price >= vehicleFilters.PriceFrom);
			if (vehicleFilters.PriceTo != null)
				vehicles = _dataContext.Vehicles.Where(x => x.Price <= vehicleFilters.PriceTo);

			if (vehicleFilters.Transmission != null)
				vehicles = _dataContext.Vehicles.Where(x => x.Transmission == vehicleFilters.Transmission);

			if (vehicleFilters.EngineCapacityFrom != null)
				vehicles = _dataContext.Vehicles.Where(x => x.EngineCapacity >= vehicleFilters.EngineCapacityFrom);
			if (vehicleFilters.EngineCapacityTo != null)
				vehicles = _dataContext.Vehicles.Where(x => x.EngineCapacity <= vehicleFilters.EngineCapacityTo);

			if (vehicleFilters.EnginePowerFrom != null)
				vehicles = _dataContext.Vehicles.Where(x => x.EnginePower >= vehicleFilters.EnginePowerFrom);
			if (vehicleFilters.EnginePowerTo != null)
				vehicles = _dataContext.Vehicles.Where(x => x.EnginePower <= vehicleFilters.EnginePowerTo);

			if (vehicleFilters.YearFrom != null)
				vehicles = _dataContext.Vehicles.Where(x => x.Year >= vehicleFilters.YearFrom);
			if (vehicleFilters.YearTo != null)
				vehicles = _dataContext.Vehicles.Where(x => x.Year <= vehicleFilters.YearTo);

			return vehicles;
		}
		public async Task<IEnumerable<Vehicle>?> GetWithFiltersAsync(VehicleFiltersRequest vehicleFilters)
		{
			var vehicles = ApplyFilters(vehicleFilters);

			const int portionSize = 12;

			if (vehicles == null)
				return null;

			if (vehicleFilters.Portion <= 0)
				vehicleFilters.Portion = 1;

			vehicles = vehicles.Skip((vehicleFilters.Portion - 1) * portionSize).Take(portionSize);

			return await vehicles.ToListAsync();
		}

		public async Task<Vehicle?> GetVehicleAsync(int vehicleId)
		{
			var vehicle = await _dataContext.Vehicles.FirstOrDefaultAsync(x=>x.Id==vehicleId);
			return vehicle;
		}

		public async Task<string?> GetOwnerEmailAsync(int vehicleId)
		{
			var vehicle = await _dataContext.Vehicles.FirstOrDefaultAsync(x=>x.Id == vehicleId);
			
			if (vehicle == null) return null;

			var owner = await _dataContext.Users.FirstOrDefaultAsync(x=>x.Id==vehicle.OwnerId);

			if (owner == null) return null;

			return owner.Email;
		}
		public async Task<bool> SaveAsync()
		{
			int savedCount = await _dataContext.SaveChangesAsync();
			return savedCount > 0 ? true : false;
		}

		public async Task<bool> AddVehicleAsync(Vehicle newVehicle)
		{
			await _dataContext.Vehicles.AddAsync(newVehicle);
			return await SaveAsync();
		}
	}
}