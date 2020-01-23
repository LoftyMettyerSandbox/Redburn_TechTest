using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace TradeDataFeed.Contexts
{
    public class TradeContext : DbContext
    {

        private readonly TradeContext _context;

        private const string connectionString = "Server=.;Database=Redburn_Lofty1;user id=sa;password=asr;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }


        //public TradeContext(DbContextOptions<TradeContext> options) : base(options) { }

        public DbSet<OMSTradeData> TradeData { get; set; }

        //public bool CommitTrades(IEnumerable<OMSTradeDataModel> trades)
        //{
        //    TradeData.AddRange(trades);
        //    SaveChanges();
        //    return true;
        //}

        public bool CommitTrade(OMSTradeData trade)
        {
            TradeData.Add(trade);
            SaveChanges();
            return true;
        }
    }
}
