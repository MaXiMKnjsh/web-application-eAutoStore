using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using web_application_eAutoStore.APPLICATION.Interfaces.Auth;
using web_application_eAutoStore.APPLICATION.Interfaces.Repositories;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using web_application_eAutoStore.DOMAIN.DTOs.Vehicles;
using web_application_eAutoStore.Enumerations;
using web_application_eAutoStore.Interfaces.Repositories;
using web_application_eAutoStore.Interfaces.Services;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Services
{
    public class VehiclesService : IVehiclesService
	{
		private readonly IVehiclesRepository _vehiclesRepository;
		private readonly IMapper _mapper;

		public VehiclesService(IVehiclesRepository vehiclesRepository, IMapper mapper)
        {
			_mapper = mapper;
			_vehiclesRepository = vehiclesRepository;
        }

		public async Task<string?> GetOwnerEmailAsync(int vehicleId)
		{
			var ownerEmail = await _vehiclesRepository.GetOwnerEmailAsync(vehicleId);
			return ownerEmail;
		}

		public async Task<int> GetQuantityAsync(VehicleFiltersRequest vehicleFilters)
		{
			var totalQuantity = await _vehiclesRepository.GetQuantityAsync(vehicleFilters);
			return totalQuantity;
		}

		public async Task<VehicleDto?> GetVehicleAsync(int vehicleId)
		{
			var vehicle = await _vehiclesRepository.GetVehicleAsync(vehicleId);

			if (vehicle == null)
				return null;

			var vehicleDto = _mapper.Map<VehicleDto>(vehicle);

			return vehicleDto;
		}

		public async Task<IEnumerable<VehicleDto>?> GetWithFiltersAsync(VehicleFiltersRequest vehicleFilters) 
        {
			var vehicles = await _vehiclesRepository.GetWithFiltersAsync(vehicleFilters);

			var vehiclesDtos = _mapper.Map<IEnumerable<VehicleDto>>(vehicles); 

			return vehiclesDtos;
		}
	}
}
