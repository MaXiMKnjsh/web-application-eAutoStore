using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace web_application_eAutoStore.Services
{
	public class VehiclesService : IVehiclesService
	{
		private readonly IVehiclesRepository _vehiclesRepository;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _appEnvironment;
		private readonly HttpClient _httpClient;
		private const string adsImagesPath = "/adsImages/";

		public VehiclesService(IVehiclesRepository vehiclesRepository, IMapper mapper, IWebHostEnvironment appEnvironment, HttpClient httpClient)
		{
			_mapper = mapper;
			_vehiclesRepository = vehiclesRepository;
			_appEnvironment = appEnvironment;
			_httpClient = httpClient;
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

		public async Task<IEnumerable<VehicleDto>?> GetWithFiltersAsync(VehicleFiltersRequest vehicleFilters, int portionSize)
		{
			var vehicles = await _vehiclesRepository.GetWithFiltersAsync(vehicleFilters, portionSize);

			var vehiclesDtos = _mapper.Map<IEnumerable<VehicleDto>>(vehicles);

			return vehiclesDtos;
		}

		public async Task<bool> AddVehicleAsync(VehicleAddRequest vehicleAddRequest, string? imagePath, int ownerId)
		{
			var newVehicle = _mapper.Map<Vehicle>(vehicleAddRequest);

			newVehicle.ImagePath = imagePath;
			newVehicle.OwnerId = ownerId;

			return await _vehiclesRepository.AddVehicleAsync(newVehicle);
		}
		public async Task<string> SaveImageAsync(IFormFile image)
		{
			string imageName = Guid.NewGuid().ToString() + ".jpg";
			string fullPath = _appEnvironment.WebRootPath + adsImagesPath + imageName;

			using (var fileStream = new FileStream(fullPath, FileMode.Create))
			{
				await image.CopyToAsync(fileStream);
			}

			return imageName;
		}

		public async Task<IEnumerable<VehicleDto>?> GetNewVehiclesAsync(int count)
		{
			var vehicles = await _vehiclesRepository.GetNewVehiclesAsync(count);

			if (vehicles == null)
				return null;

			var vehiclesDtos = _mapper.Map<IEnumerable<VehicleDto>>(vehicles);

			return vehiclesDtos;
		}

		public async Task<bool> IsAlreadySavedAsync(int vehicleId)
		{
			var result = await _vehiclesRepository.IsAlreadySavedAsync(vehicleId);
			return result;
		}

		public async Task<bool> DeleteVehicleAsync(int vehicleId)
		{
			var result = await _vehiclesRepository.DeleteVehicleAsync(vehicleId);
			return result;
		}

		public async Task<VehicleEstimates> GetMarketEstimates(int vehicleId)
		{
			string url = "";
			// Выполняем GET-запрос к API
			//HttpResponseMessage response = await _httpClient.GetAsync(url);
			// Убеждаемся, что запрос выполнен успешно
			//response.EnsureSuccessStatusCode(); // Считываем содержимое ответа в строку
			//string responseBody = await response.Content.ReadAsStringAsync();
			// Десериализуем JSON-ответ в объект
			VehicleEstimates estimates = /*JsonSerializer.Deserialize<VehicleEstimates>(responseBody) ??*/ new VehicleEstimates { AveragePrice=0, MaximumPrice=0, MinimumPrice=0};
			// Возвращаем значение AveragePrice
			return estimates;
		}
	}
}
