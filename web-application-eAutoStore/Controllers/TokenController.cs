using Microsoft.AspNetCore.Mvc;
using web_application_eAutoStore.APPLICATION.Interfaces.Auth;
using web_application_eAutoStore.Interfaces.Services;

namespace web_application_eAutoStore.Controllers
{
	public class TokenController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ITokensService _tokensService;
		private readonly IUsersService _usersService;
		public TokenController(ILogger<HomeController> logger, ITokensService tokensService, IUsersService usersService)
		{
			_logger = logger;
			_tokensService=tokensService;
			_usersService = usersService;
		}

		[HttpPost]
		public async Task<IActionResult> UpdateToken()
		{
			var result = _tokensService.GetTokensFromCookie(out string? jwtToken,out string? refreshToken);

			if (!result)
				return Unauthorized();

            var isRtValid = await _tokensService.IsRefreshTokenValid(refreshToken);

			if (!isRtValid)
				return Unauthorized();

			var user = await _usersService.GetUserByRefreshTokenAsync(refreshToken);
			var jwt = _tokensService.GenerateJWToken(user);
			var rt = (await _tokensService.GenerateRefreshTokenAsync(user, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString())).ToString();

			_tokensService.AddTokensToCookie(jwt,rt);

			return Ok();
		}
	}
}
