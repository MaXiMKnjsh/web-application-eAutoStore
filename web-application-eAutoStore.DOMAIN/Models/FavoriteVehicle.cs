namespace web_application_eAutoStore.Models
{
    public class FavoriteVehicle
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VehicleId { get; set;}
        // navigation fields
        public User User { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
