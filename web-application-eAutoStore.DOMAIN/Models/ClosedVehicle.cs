using web_application_eAutoStore.DOMAIN.Enumerations;
using web_application_eAutoStore.Enumerations;

namespace web_application_eAutoStore.Models
{
    public class ClosedVehicle
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public WayOfAttraction WayOfAttraction { get; set; }
        public string? WayDescription { get; set; }
        public WayOfSelling WayOfSelling { get; set; }
        public CarQuality? Quality { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public CarType? Type { get; set; }
        public DateTime DateOfSelling { get; set; }
        // navigation fields
        public User User { get; set; }
    }
}
