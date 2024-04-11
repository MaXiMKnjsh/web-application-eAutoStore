using Microsoft.AspNetCore.Http;
using web_application_eAutoStore.APPLICATION.Interfaces.Auth;
using web_application_eAutoStore.Enumerations;
using web_application_eAutoStore.Interfaces.Repositories;
using web_application_eAutoStore.Models;
using web_application_eAutoStore.Interfaces.Services;
using web_application_eAutoStore.DOMAIN.DTOs.Vehicles;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using AutoMapper;
using web_application_eAutoStore.DOMAIN.DTOs.FavoriteVehicles;
using System.Reflection.Metadata.Ecma335;

namespace web_application_eAutoStore.Services
{
	public class FavoriteVehiclesService : IFavoriteVehiclesService
	{
		private readonly IFavoriteVehiclesRepository _favoriteVehiclesRepository;
		private readonly IMapper _mapper;
        public FavoriteVehiclesService(IFavoriteVehiclesRepository favoriteVehiclesRepository,
			IMapper mapper)
        {
			_favoriteVehiclesRepository = favoriteVehiclesRepository;
			_mapper = mapper;
		}

		public async Task<bool> DeleteFavoriteVehicleAsync(int vehicleId, int userId)
		{
			var result = await _favoriteVehiclesRepository.DeleteFavoriteVehicleAsync(vehicleId,userId);
			return result;
		}

		public async Task<bool> DeleteFavoriteVehiclesAsync(int favoriteVehicleId)
		{
			var result = await _favoriteVehiclesRepository.DeleteFavoriteVehiclesAsync(favoriteVehicleId);
			return result;
		}

		public async Task<IEnumerable<FavVehicleDto>?> GetFavoriteVehiclesAsync(int userId)
		{
			var favVehicles = await _favoriteVehiclesRepository.GetFavoriteVehiclesAsync(userId);
			var favVehiclesDtos = _mapper.Map<IEnumerable<FavVehicleDto>>(favVehicles);
			return favVehiclesDtos;
		}

		public async Task<bool> IsAlreadySavedAsync(int userId, int favoriteVehicleId)
		{
			var result = await _favoriteVehiclesRepository.IsAlreadySavedAsync(userId, favoriteVehicleId);
				return result;
		}

		public async Task<bool> IsExist(int favoriteVehicleId)
		{
			var result = await _favoriteVehiclesRepository.IsExist(favoriteVehicleId);
				return result;
		}

		public async Task<bool> SaveFavoriteVehicleAsync(int userId, int favoriteVehicleId)
		{
			var result = await _favoriteVehiclesRepository.SaveFavoriteVehicleAsync(userId, favoriteVehicleId);
			return result;
		}
	}
}
