using Microsoft.AspNetCore.Http;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using web_application_eAutoStore.DOMAIN.DTOs.Vehicles;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Interfaces.Services
{
    public interface IUsersService
    {
		Task<bool> RegisterAsync(string name, string email, string password);
		Task<bool> IsExistAsync(string email);
		Task<bool> LoginAsync(string email, string password);
		Task<UserDto> GetUserByEmailAsync(string email);
		Task<UserDto> GetUserByRefreshTokenAsync(string refreshToken);
		Task<IEnumerable<VehicleDto>?> GetUserAdvertisementsAsync(int id);
		Task<UserDto> GetUserByIdAsync(int id);
		Task<IEnumerable<VehicleDto>?> GetAdsByIdAsync(IEnumerable<int> vehsIds);
	}
}
