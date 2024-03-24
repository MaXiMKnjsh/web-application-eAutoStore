using web_application_eAutoStore.Interfaces.Auth;
using web_application_eAutoStore.Interfaces.Repositories;
using web_application_eAutoStore.Interfaces.Services;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Services
{
    public class UsersService : IUsersService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository,IPasswordHasher passwordHasher)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> IsExistAsync(string email) => await _usersRepository.IsExistAsync(email);

        public async Task<bool> RegisterAsync(string name, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            //TODO user id autoincr and interacting with repository
            var user = new User()
            {
                Name = name,
                Email = email,
                HashedPassword = hashedPassword
            };

            return await _usersRepository.AddAsync(user);
        }
        
    }
}
