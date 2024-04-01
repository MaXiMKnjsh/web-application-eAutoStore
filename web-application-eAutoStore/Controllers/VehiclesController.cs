using Microsoft.AspNetCore.Mvc;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
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
			var vehicles = await _vehiclesService.GetWithFiltersAsync(vehicleFilters);

			var totalQuantity = await _vehiclesService.GetQuantityAsync(vehicleFilters);
			
			ViewBag.TotalQuantity = totalQuantity;
			ViewBag.Vehicles = vehicles;
			ViewBag.Portion = vehicleFilters.Portion;

			return View();
		}

		public IActionResult AddAdvertisement()
		{

			return View();
		}

	}
}
