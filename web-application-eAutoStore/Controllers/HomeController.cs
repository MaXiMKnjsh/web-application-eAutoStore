using Microsoft.AspNetCore.Mvc;
using web_application_eAutoStore.Interfaces.Services;
using web_application_eAutoStore.Services;

namespace web_application_eAutoStore.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IVehiclesService _vehiclesService;
		private const int newCarsCount = 12; 

		public HomeController(ILogger<HomeController> logger, IVehiclesService vehiclesService)
		{
			_logger = logger;
			_vehiclesService = vehiclesService;
		}

		public async Task<IActionResult> Index()
		{
			var vehicles = await _vehiclesService.GetNewVehiclesAsync(newCarsCount);

			ViewBag.Vehicles = vehicles;

			return View();
		}
	}
}
