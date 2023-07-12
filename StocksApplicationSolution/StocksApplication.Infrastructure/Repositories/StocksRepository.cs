using Microsoft.EntityFrameworkCore;
using StocksApplication.Core.Domain.Entities;
using StocksApplication.Core.Domain.RepositoryContracts;
using StocksApplication.Infrastructure.DatabaseContext;

namespace StocksApplication.Infrastructure.Repositories
{
    public class StocksRepository : IStocksRepository
    {
        // Fields
        private readonly StockMarketDbContext _db;


        // Constructors
        public StocksRepository(StockMarketDbContext db)
        {
            _db = db;
        }

        // Methods
        public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
        {
            _db.BuyOrders.Add(buyOrder);
            await _db.SaveChangesAsync();
            return buyOrder;
        }

        public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
        {
            _db.SellOrders.Add(sellOrder);
            await _db.SaveChangesAsync();
            return sellOrder;
        }

        public async Task<List<BuyOrder>> GetBuyOrders()
        {
            return await _db.BuyOrders.ToListAsync();
        }

        public async Task<List<SellOrder>> GetSellOrders()
        {
            return await _db.SellOrders.ToListAsync();
        }
    }
}
