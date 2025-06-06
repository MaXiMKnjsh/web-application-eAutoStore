﻿using Microsoft.EntityFrameworkCore;
using web_application_eAutoStore.APPLICATION.Interfaces.Repositories;
using web_application_eAutoStore.Data;
using web_application_eAutoStore.DOMAIN.DTOs.Vehicles;
using web_application_eAutoStore.DOMAIN.Models;
using web_application_eAutoStore.Enumerations;
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
			var vehicles = await ApplyFilters(vehicleFilters).ToListAsync();

			if (vehicles?.Count == null)
				return 0;

			var totalQuantity = vehicles.Count();

			return totalQuantity;
		}
		private IQueryable<Vehicle> ApplyFilters(VehicleFiltersRequest vehicleFilters)
		{
			IQueryable<Vehicle> vehicles = _dataContext.Vehicles;

			if (vehicleFilters.SearchLineRequest != null)
			{
				var partsOfRequset = vehicleFilters.SearchLineRequest.Split(new char[]{ ' '});
				foreach (var i in partsOfRequset)
				{
					vehicles = vehicles.Where(x => x.Model.ToLower().Contains(i.ToLower()));
					vehicles = vehicles.Where(x => x.Brand.ToLower().Contains(i.ToLower()));
				}
			}

			if (vehicleFilters.Model != null)
				vehicles = vehicles.Where(x => string.Equals(x.Model.ToLower(),vehicleFilters.Model.ToLower()));

			if (vehicleFilters.Brand != null)
				vehicles = vehicles.Where(x => string.Equals(x.Brand.ToLower(),vehicleFilters.Brand.ToLower()));

			if (vehicleFilters.Mileage != null)
				vehicles = vehicles.Where(x => x.Mileage == vehicleFilters.Mileage);

			if (vehicleFilters.Type != null)
				vehicles = vehicles.Where(x => x.Type == vehicleFilters.Type);

			if (vehicleFilters.Quality != null)
				vehicles = vehicles.Where(x => x.Quality == vehicleFilters.Quality);

			if (vehicleFilters.Quality != null)
				vehicles = vehicles.Where(x => x.Quality == vehicleFilters.Quality);

			if (vehicleFilters.PriceFrom != null)
				vehicles = vehicles.Where(x => x.Price >= vehicleFilters.PriceFrom);
			if (vehicleFilters.PriceTo != null)
				vehicles = vehicles.Where(x => x.Price <= vehicleFilters.PriceTo);

			if (vehicleFilters.Transmission != null)
				vehicles = vehicles.Where(x => x.Transmission == vehicleFilters.Transmission);

			if (vehicleFilters.EngineCapacityFrom != null)
				vehicles = vehicles.Where(x => x.EngineCapacity >= vehicleFilters.EngineCapacityFrom);
			if (vehicleFilters.EngineCapacityTo != null)
				vehicles = vehicles.Where(x => x.EngineCapacity <= vehicleFilters.EngineCapacityTo);

			if (vehicleFilters.EnginePowerFrom != null)
				vehicles = vehicles.Where(x => x.EnginePower >= vehicleFilters.EnginePowerFrom);
			if (vehicleFilters.EnginePowerTo != null)
				vehicles = vehicles.Where(x => x.EnginePower <= vehicleFilters.EnginePowerTo);

			if (vehicleFilters.YearFrom != null)
				vehicles = vehicles.Where(x => x.Year >= vehicleFilters.YearFrom);
			if (vehicleFilters.YearTo != null)
				vehicles = vehicles.Where(x => x.Year <= vehicleFilters.YearTo);

			return vehicles;
		}
		public async Task<IEnumerable<Vehicle>> GetWithFiltersAsync(VehicleFiltersRequest vehicleFilters, int portionSize)
		{
			var vehicles = ApplyFilters(vehicleFilters);

			if (vehicleFilters.Portion <= 0)
				vehicleFilters.Portion = 1;

			vehicles = vehicles.Skip((vehicleFilters.Portion - 1) * portionSize).Take(portionSize);

			return await vehicles.ToListAsync();
		}

		public async Task<Vehicle?> GetVehicleAsync(int vehicleId)
		{
			var vehicle = await _dataContext.Vehicles.FirstOrDefaultAsync(x => x.Id == vehicleId);
			return vehicle;
		}

		public async Task<string?> GetOwnerEmailAsync(int vehicleId)
		{
			var vehicle = await _dataContext.Vehicles.FirstOrDefaultAsync(x => x.Id == vehicleId);

			if (vehicle == null) return null;

			var owner = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == vehicle.OwnerId);

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
			var result = await _dataContext.Vehicles.AddAsync(newVehicle);

			// add vehicle to stats
			var statOfVeh = new StatOfNewAds();
			statOfVeh.UserId = newVehicle.OwnerId;
			statOfVeh.DateOf = DateTime.Now;
			statOfVeh.User = newVehicle.User;

			await _dataContext.StatsOfNewAds.AddAsync(statOfVeh);
			return await SaveAsync();
		}

		public async Task<IEnumerable<Vehicle>> GetNewVehiclesAsync(int count)
		{
			var vehicles = _dataContext.Vehicles.OrderByDescending(x => x.Id).Take(count);
			return await vehicles.ToListAsync();
		}

		public async Task<bool> IsAlreadySavedAsync(int vehicleId)
		{
			var vehicle = await _dataContext.Vehicles.FirstOrDefaultAsync(x => x.Id == vehicleId);

			if (vehicle == null)
				return false;

			return true;
		}

		public async Task<bool> DeleteVehicleAsync(int vehicleId)
		{
			var vehicleToDelete = await _dataContext.Vehicles.FirstOrDefaultAsync(x => x.Id == vehicleId);

			if (vehicleToDelete == null)
				return false;

			_dataContext.Vehicles.Remove(vehicleToDelete);

			return await SaveAsync();
		}

        public async Task<bool> AddVehicleInfoAsync(ClosedVehicle request)
        {
            var result = await _dataContext.ClosedVehicles.AddAsync(request);
			return await SaveAsync();
		}

        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            return await _dataContext.Vehicles.ToListAsync();
        }
        public async Task<List<StatOfNewAds>> GetAllStats()
        {
            return await _dataContext.StatsOfNewAds.ToListAsync();
        }

        public async Task<List<DeletedAdvertisement>> GetAllDeletedAdsData()
        {
            return await _dataContext.DeletedAdvertisements.ToListAsync();
        }

        public async Task<List<ClosedVehicle>> GetAllClosedVehiclesData()
        {
            return await _dataContext.ClosedVehicles.ToListAsync();
        }
    }
}