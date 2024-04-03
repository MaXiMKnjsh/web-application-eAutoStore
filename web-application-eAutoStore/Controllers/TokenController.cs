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
			var isContaintJwt=HttpContext.Request.Cookies.TryGetValue("jwt", out string? jwToken);
			var isContaintRt = HttpContext.Request.Cookies.TryGetValue("rt", out string? refreshToken);

			if (!isContaintJwt || !isContaintRt)
				return Unauthorized();

            var isRtValid = await _tokensService.IsRefreshTokenValid(refreshToken);

			if (!isRtValid)
				return Unauthorized();

			var user = await _usersService.GetUserByRefreshToken(refreshToken);

			var jwt = _tokensService.GenerateJWToken(user);
			var rt = await _tokensService.GenerateRefreshTokenAsync(user, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());

			HttpContext.Response.Cookies.Append("jwt", jwt);
			HttpContext.Response.Cookies.Append("rt", rt.ToString());

			return Ok();
		}
	}
}
