using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_application_eAutoStore.APPLICATION.Interfaces.Auth;
using web_application_eAutoStore.DOMAIN.DTOs.FavoriteVehicles;
using web_application_eAutoStore.Interfaces.Services;

namespace web_application_eAutoStore.Controllers
{
	public class FavoriteVehiclesController : Controller
	{
		private readonly IFavoriteVehiclesService _favoriteVehiclesService;
		private readonly ITokensService _tokensService;
		public FavoriteVehiclesController(IFavoriteVehiclesService favoriteVehiclesService,
			ITokensService tokensService)
        {
			_favoriteVehiclesService = favoriteVehiclesService;
			_tokensService= tokensService;
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetFavoriteVehicles()
		{
			var userId = _tokensService.GetUserId();

			if (userId == null)
				return Unauthorized();

			var userFavVehiclesInfo = await _favoriteVehiclesService.GetFavoriteVehiclesAsync((int)userId);

			return Ok(userFavVehiclesInfo);
		}
        [HttpPost]
		[Authorize]
		public async Task<IActionResult> SaveFavoriteVehicle([FromBody]FavoriteVehicleAddRequest favoriteVehicleAddRequest)
		{
			if (!ModelState.IsValid)
				return Unauthorized();

			if (await _favoriteVehiclesService.IsAlreadySavedAsync(favoriteVehicleAddRequest.UserId, favoriteVehicleAddRequest.VehicleId))
				return Conflict();

			var result = await _favoriteVehiclesService.SaveFavoriteVehicleAsync(favoriteVehicleAddRequest.UserId,favoriteVehicleAddRequest.VehicleId);

			if (result != true)
				return StatusCode(500);

			return Ok(result);
		}

		[HttpDelete]
		[Authorize]
		public async Task<IActionResult> DeleteFavoriteVehicle([FromQuery]int vehicleId)
		{
			var userId = _tokensService.GetUserId();

			if (userId == null)
				return Unauthorized();

			var isExist = await _favoriteVehiclesService.IsAlreadySavedAsync((int)userId,vehicleId);

			if (isExist == false)
				return BadRequest();

			var result = await _favoriteVehiclesService.DeleteFavoriteVehicleAsync(vehicleId,(int)userId);

			if (result == false)
				return StatusCode(500);

			return Ok();
		}
	}
}
