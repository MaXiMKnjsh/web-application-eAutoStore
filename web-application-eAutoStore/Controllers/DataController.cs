using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_application_eAutoStore.APPLICATION.Interfaces.Auth;
using web_application_eAutoStore.APPLICATION.Services;
using web_application_eAutoStore.Interfaces.Services;

namespace web_application_eAutoStore.Controllers
{
    public class DataController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IVehiclesService _vehiclesService;

        public DataController(IUsersService usersService,
            IVehiclesService vehiclesService)
        {
            _usersService = usersService;
            _vehiclesService = vehiclesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            var result = await _vehiclesService.GetAllVehiclesAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStats()
        {
            var result = await _vehiclesService.GetAllStats();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsersData()
        {
            var result = await _usersService.GetAllUsersData();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDeletedAdsData()
        {
            var result = await _vehiclesService.GetAllDeletedAdsData();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClosedVehiclesData()
        {
            var result = await _vehiclesService.GetAllClosedVehiclesData();
            return Ok(result);
        }
    }
}
