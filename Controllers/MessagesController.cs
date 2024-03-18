using Microsoft.AspNetCore.Mvc;

namespace web_application_eAutoStore.Controllers
{
	public class MessagesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
