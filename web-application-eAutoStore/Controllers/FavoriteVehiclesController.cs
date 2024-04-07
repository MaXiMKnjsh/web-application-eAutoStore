using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_application_eAutoStore.DOMAIN.DTOs.FavoriteVehicles;
using web_application_eAutoStore.Interfaces.Services;

namespace web_application_eAutoStore.Controllers
{
	public class FavoriteVehiclesController : Controller
	{
		private readonly IFavoriteVehiclesService _favoriteVehiclesService;
		public FavoriteVehiclesController(IFavoriteVehiclesService favoriteVehiclesService)
        {
			_favoriteVehiclesService = favoriteVehiclesService;

		}
        [HttpPost]
		[Authorize]
		public async Task<IActionResult> SaveFavoriteVehicle([FromBody]FavoriteVehicleAddRequest favoriteVehicleAddRequest)
		{
			if (!ModelState.IsValid)
				return Unauthorized();

			var result = await _favoriteVehiclesService.SaveFavoriteVehicleAsync(favoriteVehicleAddRequest.UserId,favoriteVehicleAddRequest.VehicleId);

			if (result != true)
				return StatusCode(500);

			return Ok(result);
		}
	}
}
