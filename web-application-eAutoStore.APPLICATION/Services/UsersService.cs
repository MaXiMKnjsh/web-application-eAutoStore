using Microsoft.AspNetCore.Http;
using web_application_eAutoStore.APPLICATION.Interfaces.Auth;
using web_application_eAutoStore.Enumerations;
using web_application_eAutoStore.Interfaces.Repositories;
using web_application_eAutoStore.Interfaces.Services;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Services
{
    public class UsersService : IUsersService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUsersRepository _usersRepository;
        private readonly IJwtProvider _jwtProvider;
        public UsersService(IUsersRepository usersRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider= jwtProvider;
        }

        public async Task<bool> IsExistAsync(string email) => await _usersRepository.IsExistAsync(email);

        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _usersRepository.GetByEmailAsync(email);

            var result = _passwordHasher.Verify(password, user.HashedPassword);

            if (!result)
                return null;

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }

        public async Task<bool> RegisterAsync(string name, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = new User()
            {
                Name = name,
                Email = email,
                HashedPassword = hashedPassword,
                Role = UserRole.User
            };

            return await _usersRepository.AddAsync(user);
        }

    }
}
