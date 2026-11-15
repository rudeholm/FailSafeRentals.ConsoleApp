using Microsoft.EntityFrameworkCore;

namespace FailSafeRentals.ConsoleApp.Data
{
    public class FailSafeRentalsDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<Rental> Rentals { get; set; } = null!;
        public DbSet<ExtraFeature> ExtraFeatures { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=(localdb)\MSSQLLocalDB;"
                + "Initial Catalog=FailSafeRentals;"
                + "Integrated Security=True;"
                + "Connect Timeout=30;"
                + "Encrypt=True;"
                + "Trust Server Certificate=False;"
                + "Application Intent=ReadWrite;"
                + "Multi Subnet Failover=False;"
                + "Command Timeout=30"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Unikt index på registreringsnummer (stoppar dubletter i SQL)
            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.RegistrationNumber).IsUnique();

            // 2. Explicit precision for pengar i LocalDB
            modelBuilder.Entity<Vehicle>()
                .Property(v => v.DailyRate).HasPrecision(10, 2);

            // 3. Shadow-property för SecOps-spårbarhet
            modelBuilder.Entity<Rental>()
                .Property<DateTime>("CreatedAt").HasDefaultValueSql("GETUTCDATE()");

            // 4. Skydda uthyrningshistorik mot oavsiktlig radering
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer).WithMany(c => c.Rentals)
                .OnDelete(DeleteBehavior.Restrict);

            // Lägg till statiska data
            modelBuilder.Entity<ExtraFeature>().HasData(
                new ExtraFeature { Id = 1, Name = "GPS Navigation", DailyPrice = 75m },
                new ExtraFeature { Id = 2, Name = "Takbox Thule", DailyPrice = 120m },
                new ExtraFeature { Id = 3, Name = "Barnstol", DailyPrice = 50m }
            );
        }
    }
}
