using Microsoft.AspNetCore.Http;
using web_application_eAutoStore.APPLICATION.Interfaces.Auth;
using web_application_eAutoStore.Enumerations;
using web_application_eAutoStore.Interfaces.Repositories;
using web_application_eAutoStore.Models;
using web_application_eAutoStore.Interfaces.Services;
using web_application_eAutoStore.DOMAIN.DTOs.Vehicles;
using web_application_eAutoStore.DOMAIN.DTOs.Users;
using AutoMapper;

namespace web_application_eAutoStore.Services
{
	public class UsersService : IUsersService
	{
		private readonly IPasswordHasher _passwordHasher;
		private readonly IUsersRepository _usersRepository;
		private readonly IMapper _mapper;

		public UsersService(IUsersRepository usersRepository,
			IPasswordHasher passwordHasher,
			IMapper mapper)
		{
			_usersRepository = usersRepository;
			_passwordHasher = passwordHasher;
			_mapper = mapper;
		}

		public async Task<IEnumerable<VehicleDto>?> GetAdsByIdAsync(IEnumerable<int>? vehsIds)
		{
			if (vehsIds == null)
				return null;

			var vehicles = await _usersRepository.GetAdsByIdAsync(vehsIds);
			var vehiclesDtos = _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
			return vehiclesDtos;
		}

		public async Task<IEnumerable<VehicleDto>?> GetUserAdvertisementsAsync(int id)
		{
			var vehicles = await _usersRepository.GetUserAdvertisementsAsync(id);
			var vehiclesDtos = _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
			return vehiclesDtos;
		}

		public async Task<UserDto> GetUserByEmailAsync(string email)
		{
			var user = await _usersRepository.GetByEmailAsync(email);
			var userDto = _mapper.Map<UserDto>(user);
			return userDto;
		}

		public async Task<UserDto> GetUserByIdAsync(int id)
		{
			var user = await _usersRepository.GetUserByIdAsync(id);
			var userDto = _mapper.Map<UserDto>(user);
			return userDto;
		}

		public async Task<UserDto> GetUserByRefreshTokenAsync(string refreshToken)
		{
			var user = await _usersRepository.GetUserByRefreshToken(refreshToken);
			var userDto = _mapper.Map<UserDto>(user);
			return userDto;
		}

		public async Task<bool> IsExistAsync(string email) => await _usersRepository.IsExistAsync(email);

		public async Task<bool> LoginAsync(string email, string password)
		{
			var user = await _usersRepository.GetByEmailAsync(email);

			var result = _passwordHasher.Verify(password, user.HashedPassword);

			return result;
		}

		public async Task<bool> RegisterAsync(string name, string email, string password)
		{
			var hashedPassword = _passwordHasher.Generate(password);

			var user = new User()
			{
				Name = name,
				Email = email,
				HashedPassword = hashedPassword,
				Role = UserRole.User,
				DateOfRegistration = DateTime.Now
			};

			return await _usersRepository.AddAsync(user);
		}

		public async Task<bool> UpdateUserInfoAsync(UpdateUserRequest updateUserRequest, int userId)
		{
			string hashedPassword = null;
			if (updateUserRequest.Password != null && updateUserRequest.RepeatPassword != null)
			{
				if (!updateUserRequest.Password.Equals(updateUserRequest.RepeatPassword))
					return false;
				hashedPassword = _passwordHasher.Generate(updateUserRequest.Password);
			}

			var result = await _usersRepository.UpdateUserInfoAsync(updateUserRequest, userId, hashedPassword);
			return result;
		}
	}
}
