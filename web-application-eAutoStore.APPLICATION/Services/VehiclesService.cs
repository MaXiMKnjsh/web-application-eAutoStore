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
using web_application_eAutoStore.DOMAIN.Models;
using web_application_eAutoStore.DOMAIN.Enumerations;

namespace web_application_eAutoStore.Services
{
	public class VehiclesService : IVehiclesService
	{
		private readonly IDeletedAdvertismentsRepository _deletedAdvsRepository;
		private readonly IVehiclesRepository _vehiclesRepository;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _appEnvironment;
		private readonly HttpClient _httpClient;
		private const string adsImagesPath = "/adsImages/";

		public VehiclesService(IVehiclesRepository vehiclesRepository, IMapper mapper, IWebHostEnvironment appEnvironment, HttpClient httpClient,IDeletedAdvertismentsRepository deletedAdvsRepository)
		{
			_mapper = mapper;
			_vehiclesRepository = vehiclesRepository;
			_deletedAdvsRepository = deletedAdvsRepository;
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

        public async Task<bool> DeleteVehicleWithReasonAsync(DeleteVehicleRequest request)
        {
            var result = await _vehiclesRepository.DeleteVehicleAsync(request.VehicleId);

			var result2 = false;

			if (result)
			{
				var deletedAdvertisement = new DeletedAdvertisement();

				ReasonOfRemoving reasonEnum;
				if (request.ReasonEnum == "Sold") {
					reasonEnum =ReasonOfRemoving.Sold;
                } else if (request.ReasonEnum == "ChangedMind") {
                    reasonEnum = ReasonOfRemoving.ChangedMind;
                } else if (request.ReasonEnum == "DislikePlatform") {
                    reasonEnum = ReasonOfRemoving.DislikePlatform;
                }
				else {
                    reasonEnum = ReasonOfRemoving.Another;
                }

                deletedAdvertisement.RemovingDescrtiption = request.Reason;
                deletedAdvertisement.ReasonOfRemoving = reasonEnum;
				deletedAdvertisement.DateOf = DateTime.Now;

                result2 = await _deletedAdvsRepository.AddReasonOfRemovingAsync(deletedAdvertisement);
			}

			return result2;
        }

        public async Task<bool> AddVehicleInfoAsync(ClosedVehicleRequest request, int userId)
        {
			var data = new ClosedVehicle();
			WayOfAttraction wayOfAttraction;
            if (request.WayOfAttraction == "SocialNetwork"){
				wayOfAttraction = WayOfAttraction.SocialNetwork;
            } else if (request.WayOfAttraction == "Friend"){
                wayOfAttraction = WayOfAttraction.Friend;
            } else if (request.WayOfAttraction == "ContextAd"){
                wayOfAttraction = WayOfAttraction.ContextAd;
            }
			else{
                wayOfAttraction = WayOfAttraction.Search;
            }
			data.WayOfAttraction = wayOfAttraction;
            data.WayDescription = request.WayDescription;
            WayOfSelling wayOfSelling;
            if (request.WayOfSelling == "Credit")
            {
                wayOfSelling = WayOfSelling.Credit;
            }
            else if (request.WayOfSelling == "FullPrice")
            {
                wayOfSelling = WayOfSelling.FullPrice;
            }
            else if (request.WayOfSelling == "Leasing")
            {
                wayOfSelling = WayOfSelling.Leasing;
            }
            else
            {
                wayOfSelling = WayOfSelling.Installments;
            }
            data.WayOfSelling = wayOfSelling;
            CarQuality carQuality;
            if (request.Quality == 2) {
                carQuality = CarQuality.Repairable;
            }
            else if (request.Quality == 0) {
                carQuality = CarQuality.New;
            }
            else {
                carQuality = CarQuality.Used;
            }
            data.Quality = carQuality;
            data.Brand = request.Brand;
            data.Model = request.Model;
			CarType carType;
            if (request.Type == 0)
            {
                carType = CarType.Passenger;
            }
            else if (request.Type == 1)
            {
                carType = CarType.Motorcycles;
            }
            else if (request.Type == 2)
            {
                carType = CarType.Trucks;
            }
            else if (request.Type == 3)
            {
                carType = CarType.Special;
            }
            else if (request.Type == 4)
            {
                carType = CarType.Buses;
            }
            else
            {
                carType = CarType.Other;
            }
            data.Type = carType;
            data.DateOfSelling = DateTime.Now;
			data.UserId = userId;

            return await _vehiclesRepository.AddVehicleInfoAsync(data);          
        }
    }
}
