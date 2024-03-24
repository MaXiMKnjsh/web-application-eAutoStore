using Microsoft.EntityFrameworkCore;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<FavoriteVehicle> FavoriteVehicles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dialog>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Message>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<User>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<FavoriteVehicle>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Vehicle>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Dialog>()
                .HasMany(m => m.Messages)
                .WithOne(d => d.Dialog)
                .HasForeignKey(di => di.DialogId);

            modelBuilder.Entity<Message>()
                .HasOne(u => u.Receiver)
                .WithMany(si => si.ReceivedMessages)
                .HasForeignKey(i=>i.Id);

            modelBuilder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(si => si.SentMessages)
                .HasForeignKey(i => i.Id);

            modelBuilder.Entity<FavoriteVehicle>()
                .HasOne(u => u.User)
                .WithMany(fv => fv.FavoriteVehicles)
                .HasForeignKey(i => i.Id);

            modelBuilder.Entity<Vehicle>()
                .HasMany(fv => fv.FavoriteVehicles)
                .WithOne(v => v.Vehicle)
                .HasForeignKey(v => v.VehicleId);

            modelBuilder.Entity<Vehicle>()
                .HasOne(u => u.User)
                .WithMany(v => v.Vehicles)
                .HasForeignKey(i => i.Id);

            //TODO create validation
        }
    }
}
