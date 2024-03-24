using BCrypt.Net;
using web_application_eAutoStore.Interfaces.Auth;

namespace web_application_eAutoStore.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Generate(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool Verify(string password, string hashedPassword) => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}
