using web_application_eAutoStore.DOMAIN.Models;
using web_application_eAutoStore.Enumerations;

namespace web_application_eAutoStore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Surname { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        // navigation fields
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<FavoriteVehicle> FavoriteVehicles { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
