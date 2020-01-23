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
        public DbSet<TradeMessage> Messages { get; set; }


        public bool CommitTrade(OMSTradeData trade)
        {
            TradeData.Add(trade);
            SaveChanges();
            return true;
        }

        public bool CommitMessage(TradeMessage message)
        {
            Messages.AddRange(message);
            SaveChanges();
            return true;
        }
    }
}
