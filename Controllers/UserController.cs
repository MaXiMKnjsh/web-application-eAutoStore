using Microsoft.AspNetCore.Mvc;

namespace web_application_eAutoStore.Controllers
{
	public class UserController : Controller
	{
		public IActionResult Login()
		{
			return View();
		}
	}
}
