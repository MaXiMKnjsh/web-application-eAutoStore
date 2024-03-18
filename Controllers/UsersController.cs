using Microsoft.AspNetCore.Mvc;

namespace web_application_eAutoStore.Controllers
{
	public class UsersController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
