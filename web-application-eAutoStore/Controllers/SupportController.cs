using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace web_application_eAutoStore.Controllers
{
	public class SupportController : Controller
	{
		[Authorize]
		public IActionResult Index()
		{
			return View();
		}
	}
}
