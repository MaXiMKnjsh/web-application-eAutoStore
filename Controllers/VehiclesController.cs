using Microsoft.AspNetCore.Mvc;

namespace web_application_eAutoStore.Controllers
{
	public class VehiclesController : Controller
	{
        [HttpGet]
		public IActionResult Index()
		{
			return View();
		}
    }
}
