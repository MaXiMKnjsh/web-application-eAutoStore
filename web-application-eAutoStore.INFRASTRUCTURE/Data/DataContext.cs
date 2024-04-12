using Microsoft.EntityFrameworkCore;
using web_application_eAutoStore.DOMAIN.Models;
using web_application_eAutoStore.Models;

namespace web_application_eAutoStore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<FavoriteVehicle> FavoriteVehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<RefreshToken> RefreshTokens {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<FavoriteVehicle>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Vehicle>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<RefreshToken>()
                .HasKey(d => d.Guid);

            modelBuilder.Entity<User>()
                .HasMany(m => m.RefreshTokens)
                .WithOne(d => d.User)
                .HasForeignKey(di => di.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(m => m.FavoriteVehicles)
                .WithOne(d => d.User)
                .HasForeignKey(di => di.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(m => m.Vehicles)
                .WithOne(d => d.User)
                .HasForeignKey(di => di.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vehicle>()
                .HasMany(m => m.FavoriteVehicles)
                .WithOne(d => d.Vehicle)
                .HasForeignKey(di => di.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RefreshToken>(builder =>
            {
                builder.Property(x => x.Guid).IsRequired().HasDefaultValueSql("NEWID()");
                builder.Property(x => x.UserId).IsRequired();
                builder.Property(x => x.ExpiringAt).IsRequired();
                builder.Property(x => x.GeneratedAt).IsRequired();
                builder.Property(x => x.AssociatedDeviceName).IsRequired();
            });

            modelBuilder.Entity<FavoriteVehicle>(builder =>
            {
                builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
                builder.Property(x => x.UserId).IsRequired();
                builder.Property(x => x.VehicleId).IsRequired();
            });

            modelBuilder.Entity<User>(builder =>
            {
                builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
                builder.Property(x => x.Name).IsRequired();
                builder.Property(x => x.Surname).IsRequired(false);
                builder.Property(x => x.HashedPassword).IsRequired();
                builder.Property(x => x.Email).IsRequired();
            });

            modelBuilder.Entity<Vehicle>(builder =>
            {
                builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
                builder.Property(x => x.Brand).IsRequired();
                builder.Property(x => x.Model).IsRequired();
                builder.Property(x => x.Type).IsRequired(false);
                builder.Property(x => x.Price).IsRequired();
                builder.Property(x => x.OwnerId).IsRequired();
				builder.Property(x => x.Mileage).IsRequired();
				builder.Property(x => x.Quality).IsRequired(false);
                builder.Property(x => x.Transmission).IsRequired(false);
                builder.Property(x => x.Year).IsRequired(false);
                builder.Property(x => x.EngineCapacity).IsRequired(false);
                builder.Property(x => x.EnginePower).IsRequired(false);
				builder.Property(x => x.ImagePath).IsRequired(false);
			});
        }
    }
}
