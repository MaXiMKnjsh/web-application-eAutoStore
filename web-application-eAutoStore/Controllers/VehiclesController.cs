using Microsoft.AspNetCore.Mvc;
using web_application_eAutoStore.DOMAIN.DTOs.Vehicles;
using web_application_eAutoStore.Interfaces.Services;

namespace web_application_eAutoStore.Controllers
{
	public class VehiclesController : Controller
	{
		private readonly IVehiclesService _vehiclesService;

		public VehiclesController(IVehiclesService vehiclesService)
        {
			_vehiclesService = vehiclesService;
		}
        [HttpGet]
		public async Task<IActionResult> Index([FromQuery]VehicleFiltersRequest vehicleFilters)
		{
			var vehiclesDtos = await _vehiclesService.GetWithFiltersAsync(vehicleFilters);

			var totalQuantity = await _vehiclesService.GetQuantityAsync(vehicleFilters);
			
			ViewBag.TotalQuantity = totalQuantity;
			ViewBag.VehiclesDtos = vehiclesDtos;
			ViewBag.Portion = vehicleFilters.Portion;

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetVehicleDetailsPartial(int vehicleId)
		{
			var vehicleDto = await _vehiclesService.GetVehicleAsync(vehicleId);
			var ownerEmail = await _vehiclesService.GetOwnerEmailAsync(vehicleId);

			if (vehicleDto == null || ownerEmail == null)
				return StatusCode(500);

			ViewBag.VehicleDto = vehicleDto;
			ViewBag.OwnerEmail = ownerEmail;

			return PartialView("VehicleReviewBody");
		}

		public IActionResult AddAdvertisement()
		{

			return View();
		}

	}
}
