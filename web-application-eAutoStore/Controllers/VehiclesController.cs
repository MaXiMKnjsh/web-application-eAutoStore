using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Debugger.Contracts.HotReload;
using web_application_eAutoStore.APPLICATION.Interfaces.Auth;
using web_application_eAutoStore.APPLICATION.Services;
using web_application_eAutoStore.DOMAIN.DTOs.Vehicles;
using web_application_eAutoStore.Interfaces.Services;

namespace web_application_eAutoStore.Controllers
{
	public class VehiclesController : Controller
	{
		private readonly IVehiclesService _vehiclesService;
		private readonly IImageService _imageService;
		private readonly ITokensService _tokensService;

		public VehiclesController(IVehiclesService vehiclesService,
			IImageService imageService,
			ITokensService tokensService)
		{
			_vehiclesService = vehiclesService;
			_imageService = imageService;
			_tokensService = tokensService;
		}
		public IActionResult AddAdvertisement() => View();

		[HttpGet]
		public async Task<IActionResult> Index([FromQuery] VehicleFiltersRequest vehicleFilters)
		{
			var vehiclesDtos = await _vehiclesService.GetWithFiltersAsync(vehicleFilters);

			var totalQuantity = await _vehiclesService.GetQuantityAsync(vehicleFilters);

			ViewBag.TotalQuantity = totalQuantity;
			ViewBag.VehiclesDtos = vehiclesDtos;
			ViewBag.Portion = vehicleFilters.Portion;

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetVehicleDetailsPartial([FromQuery]int vehicleId)
		{
			var vehicleDto = await _vehiclesService.GetVehicleAsync(vehicleId);
			var ownerEmail = await _vehiclesService.GetOwnerEmailAsync(vehicleId);

			if (vehicleDto == null || ownerEmail == null)
				return StatusCode(500);

			ViewBag.VehicleDto = vehicleDto;
			ViewBag.OwnerEmail = ownerEmail;

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

			return RedirectToPage("Index");
		}
	}
}
