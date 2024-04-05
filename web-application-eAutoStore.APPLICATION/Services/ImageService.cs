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

namespace web_application_eAutoStore.Services
{
	public class ImageService : IImageService
	{
		private readonly IWebHostEnvironment _appEnvironment;
		private readonly IVehiclesRepository _vehiclesRepository;

		public ImageService(IWebHostEnvironment appEnvironment, IVehiclesRepository vehiclesRepository)
		{
			_appEnvironment = appEnvironment;
			_vehiclesRepository = vehiclesRepository;
		}
		public Task<string> GetImagePathAsync(int id)
		{
			return null;
		}

		public async Task<string> SaveImageAsync(IFormFile image)
		{
			string name = Guid.NewGuid().ToString();
			string path = "/adsImages/" + name;

			using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
			{
				await image.CopyToAsync(fileStream);
			}

			return name;
		}
	}
}
