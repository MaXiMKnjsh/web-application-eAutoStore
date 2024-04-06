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
        private readonly IMapper _mapper;

        public UserController(IUsersService usersService, ITokensService tokensService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
            _tokensService = tokensService;
        }
        public IActionResult Login() => View();
        public IActionResult Register() => View();

        //[Authorize]
        public async Task<IActionResult> Profile()
        {
            //var userId = _tokensService.GetUserId();
            var userId = 2;

            if (userId == null)
                return Unauthorized();

            var userInfo = await _usersService.GetUserByIdAsync((int)userId);
            var userAdvertisements = await _usersService.GetUserAdvertisementsAsync((int)userId);

            ViewBag.UserInfo = userInfo;
            ViewBag.UserAdvertisements = userAdvertisements;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessRegisterForm([FromForm]RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            bool isUserExist = await _usersService.IsExistAsync(request.Email);

            if (isUserExist)
                return Conflict();

            bool isSuccessRegistration = await _usersService.RegisterAsync(request.Name, request.Email, request.Password);

            if (!isSuccessRegistration)
                return StatusCode(500);

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> ProcessLoginForm([FromForm]LoginUserRequest request)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Login");

            bool isUserExist = await _usersService.IsExistAsync(request.Email);

            if (!isUserExist)
                return StatusCode(403);

            var result = await _usersService.LoginAsync(request.Email, request.Password);

            if (!result)
                return StatusCode(500);

            var user = await _usersService.GetUserByEmailAsync(request.Email);

            var jwt = _tokensService.GenerateJWToken(user);
            var rt = await _tokensService.GenerateRefreshTokenAsync(user, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());

            HttpContext.Response.Cookies.Append("jwt", jwt);
            HttpContext.Response.Cookies.Append("rt", rt.ToString());

            return RedirectToAction("Index", "Home");
        }
    }
}
