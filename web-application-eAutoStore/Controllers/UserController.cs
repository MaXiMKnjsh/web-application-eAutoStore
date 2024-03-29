﻿using Microsoft.AspNetCore.Mvc;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
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
        public async Task<IActionResult> ProcessRegisterForm([FromForm]RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Register");

            bool isUserExist = await _usersService.IsExistAsync(request.Email);

            if (isUserExist)
                return RedirectToAction("Register");
            //return Conflict();

            bool isSuccessRegistration = await _usersService.RegisterAsync(request.Name, request.Email, request.Password);

            if (!isSuccessRegistration)
                return RedirectToAction("Register");
            //return StatusCode(500);

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> ProcessLoginForm([FromForm]LoginUserRequest request)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Login");

            bool isUserExist = await _usersService.IsExistAsync(request.Email);

            if (!isUserExist)
                return RedirectToAction("Login");

            bool isSuccessLogin = await _usersService.LoginAsync(request.Email, request.Password);

            if (!isSuccessLogin)
                return RedirectToAction("Login");
            //return StatusCode(500);

            return RedirectToAction("Index","Home");
        }
    }
}
