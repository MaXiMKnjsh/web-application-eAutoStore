using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_application_eAutoStore.APPLICATION.Interfaces.Auth;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using web_application_eAutoStore.DOMAIN.Models;
using web_application_eAutoStore.Interfaces.Services;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly ITokensService _tokensService;
        private readonly IFavoriteVehiclesService _favoriteVehiclesService;
        private readonly IMapper _mapper;

        public UserController(IUsersService usersService, 
            ITokensService tokensService, 
            IMapper mapper,
            IFavoriteVehiclesService favoriteVehiclesService)
        {
            _usersService = usersService;
            _mapper = mapper;
            _tokensService = tokensService;
            _favoriteVehiclesService = favoriteVehiclesService;
        }
        public IActionResult Login() => View();
        public IActionResult Register() => View();

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = _tokensService.GetUserId();

            if (userId == null)
				RedirectToAction("Login");

			var userAdvertisements = await _usersService.GetUserAdvertisementsAsync((int)userId);
            var userInfo = await _usersService.GetUserByIdAsync((int)userId);
            var userFavVehiclesInfo = await _favoriteVehiclesService.GetFavoriteVehiclesAsync((int)userId);

            var favVehsIds = userFavVehiclesInfo?.Select(x => x.VehicleId).ToList() ?? null;

            var useFavVehicles = await _usersService.GetAdsByIdAsync(favVehsIds);

            ViewBag.UserInfo = userInfo;
            ViewBag.UserAdvertisements = userAdvertisements;
            ViewBag.FavoriteVehicles = useFavVehicles;

			return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdatePersonalInfo([FromForm]UpdateUserRequest updateUserRequest)
        {
            var userId = _tokensService.GetUserId();

            if (userId == null)
				RedirectToAction("Login");

			var result = await _usersService.UpdateUserInfoAsync(updateUserRequest, (int)userId);

            if (result == false)
				RedirectToAction("Profile");

			return RedirectToAction("Profile");
        }

        [Authorize]
		[HttpGet]
        public IActionResult GetUserId()
        {
            var userId = _tokensService.GetUserId();

            if (userId == null)
                return Unauthorized();

            return Ok((int)userId);
        }

		[HttpPost]
        public async Task<IActionResult> ProcessRegisterForm([FromForm]RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
				return RedirectToAction("Register");

			bool isUserExist = await _usersService.IsExistAsync(request.Email);

            if (isUserExist)
				return RedirectToAction("Register");

			bool isSuccessRegistration = await _usersService.RegisterAsync(request.Name, request.Email, request.Password);

            if (!isSuccessRegistration)
                return RedirectToAction("Register");

			return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> ProcessLoginForm([FromForm]FavoriteVehicleAddRequest request)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Login");

            bool isUserExist = await _usersService.IsExistAsync(request.Email);

            if (!isUserExist)
                return RedirectToAction("Login");

			var result = await _usersService.LoginAsync(request.Email, request.Password);

            if (!result)
                return RedirectToAction("Login");

			var user = await _usersService.GetUserByEmailAsync(request.Email);

            var jwt = _tokensService.GenerateJWToken(user);
            var rt = await _tokensService.GenerateRefreshTokenAsync(user, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());

            HttpContext.Response.Cookies.Append("jwt", jwt);
            HttpContext.Response.Cookies.Append("rt", rt.ToString());

            return RedirectToAction("Index", "Home");
        }
    }
}
