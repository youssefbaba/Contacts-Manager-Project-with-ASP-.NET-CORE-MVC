using CitiesManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {

        // Properties
        public virtual DbSet<City> Cities { get; set; }

        // Constructors
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        // Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // To map DbSet City to Cities database table
            modelBuilder.Entity<City>().ToTable(nameof(Cities));

            // To add seed data to Cities database table
            modelBuilder.Entity<City>().HasData(
            new City { CityID = Guid.Parse("DBF8B33A-35B0-4A1F-BD2C-0184A46515DA"), CityName = "New York" },
            new City { CityID = Guid.Parse("A9407DF9-7AB0-43E6-A311-56F9BA8B8545"), CityName = "London" }
            );

        }

    }
}
