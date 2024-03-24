using Microsoft.AspNetCore.Mvc;
using web_application_eAutoStore.DTOs.Users;
using web_application_eAutoStore.Interfaces.Services;

namespace web_application_eAutoStore.Controllers
{
	public class UserController : Controller
	{
        private readonly IUsersService _usersService;
        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        public IActionResult Login() => View();
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> ProcessRegisterForm(RegisterUserRequest request)
        {
            bool isUserExist = await _usersService.IsExistAsync(request.Email);

            if (isUserExist)
                return Conflict();

            bool isSuccessRegistration = await _usersService.RegisterAsync(request.Name, request.Email, request.Password);

            if (!isSuccessRegistration) 
                return StatusCode(500);

            return Ok();
        }
    }
}
