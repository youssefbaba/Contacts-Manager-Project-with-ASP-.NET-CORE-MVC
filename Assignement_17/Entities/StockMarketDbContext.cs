using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class StockMarketDbContext : DbContext
    {
        // Properties
        public DbSet<BuyOrder> BuyOrders { get; set; }
        public DbSet<SellOrder> SellOrders { get; set; }

        // Constructors
        public StockMarketDbContext(DbContextOptions options) : base(options)
        {
        }

        // Methdos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // To bind the model class with corresponding database table
            modelBuilder.Entity<BuyOrder>().ToTable(nameof(BuyOrders));
            modelBuilder.Entity<SellOrder>().ToTable(nameof(SellOrders));
        }

    }
}
