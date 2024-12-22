using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_application_eAutoStore.APPLICATION.Interfaces.Auth;
using web_application_eAutoStore.DOMAIN.DTOs.Vehicles;
using web_application_eAutoStore.Interfaces.Services;

namespace web_application_eAutoStore.Controllers
{
	public class VehiclesController : Controller
	{
		private readonly IVehiclesService _vehiclesService;
		private readonly ITokensService _tokensService;
		private readonly IFavoriteVehiclesService _favoriteVehiclesService;
		private const int portionSize = 12;
		public VehiclesController(IVehiclesService vehiclesService,
			ITokensService tokensService,
			IFavoriteVehiclesService favoriteVehiclesService)
		{
			_vehiclesService = vehiclesService;
			_favoriteVehiclesService = favoriteVehiclesService;
			_tokensService = tokensService;
		}
		public IActionResult AddAdvertisement() => View();

		[HttpGet]
		public async Task<IActionResult> Index([FromQuery] VehicleFiltersRequest vehicleFilters)
		{
			var vehiclesDtos = await _vehiclesService.GetWithFiltersAsync(vehicleFilters, portionSize);

			var totalQuantity = await _vehiclesService.GetQuantityAsync(vehicleFilters);

			ViewBag.TotalQuantity = totalQuantity;
			ViewBag.VehiclesDtos = vehiclesDtos;
			ViewBag.Portion = vehicleFilters.Portion;
			ViewBag.PortionSize = portionSize;

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetVehicleDetailsPartial([FromQuery]int vehicleId)
		{
			var vehicleDto = await _vehiclesService.GetVehicleAsync(vehicleId);
			var ownerEmail = await _vehiclesService.GetOwnerEmailAsync(vehicleId);

			if (vehicleDto == null || ownerEmail == null)
				return StatusCode(500);

			ViewBag.VehicleEstimates = await _vehiclesService.GetMarketEstimates(vehicleId);
			ViewBag.VehicleDto = vehicleDto;
			ViewBag.OwnerEmail = ownerEmail;
			ViewBag.ImageName = vehicleDto.ImagePath;

			return PartialView("VehicleReviewBody");
		}
		

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddNewAdvertisement([FromForm]VehicleAddRequest vehicleAddRequest)
		{
			if (!ModelState.IsValid)
				return ValidationProblem();

			string? imageName = null;
			if (vehicleAddRequest.Image != null)
			{
				imageName = await _vehiclesService.SaveImageAsync(vehicleAddRequest.Image);
			}

			var ownerId = _tokensService.GetUserId();

			if (ownerId == null)
				return StatusCode(500);

			var result = await _vehiclesService.AddVehicleAsync(vehicleAddRequest, imageName, (int)ownerId);

			if (result == false)
				return StatusCode(500);

			return RedirectToAction("Index","Home");
		}

		[HttpDelete]
		[Authorize]
		public async Task<IActionResult> DeleteVehicle([FromQuery] int vehicleId)
		{
			var userId = _tokensService.GetUserId();
			
			if (userId == null)
				return Unauthorized();

			var isExist = await _vehiclesService.IsAlreadySavedAsync(vehicleId);

			if (isExist == false)
				return BadRequest();

			//---
			// in order to fix the multiple cascade deleting i decided that
			// a manual removing it is suitable solution for my situation :)
			var isExistInFavVehs = await _favoriteVehiclesService.IsExist(vehicleId);

			if (isExistInFavVehs == true)
				await _favoriteVehiclesService.DeleteFavoriteVehiclesAsync(vehicleId);

			//---

			var result = await _vehiclesService.DeleteVehicleAsync(vehicleId);

			if (result == false)
				return StatusCode(500);

			return Ok();
		}
	}
}
