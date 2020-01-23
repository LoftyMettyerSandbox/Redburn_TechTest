using Common.Models;
using NServiceBus;
using System;
using System.Threading.Tasks;
using TradeDataFeed.Contexts;

namespace TradeDataAPI.Handlers
{
    public class OMSTradeDataHandler : IHandleMessages<OMSTradeData>
    {
        TradeContext _context;
        public OMSTradeDataHandler() {

        //    //var configuration = new ConfigurationBuilder()
        //    //          .SetBasePath(Directory.GetCurrentDirectory())
        //    //          .AddJsonFile("appsettings.json")
        //    //          .Build();
        //    //optionsBuilder.UseSqlServer(configuration["ConnectionStrings:OpenHR"]);

        //    ////_context = new TradeContext(options => {
        //    ////              options.UseSqlServer("Server=.;Database=Redburn_Lofty1;user id=sa;password=asr;")
        //    ////    }

        //    //  DbContextOptionsBuilder options = new DbContextOptionsBuilder();
        //    //    options.UseSqlServer("Server=.;Database=Redburn_Lofty1;user id=sa;password=asr;");


        //    //    services.AddDbContext<TradeContext>(options => {
        //    //      options.UseSqlServer("Server=.;Database=Redburn_Lofty1;user id=sa;password=asr;");

            _context = new TradeContext();

        }

        public Task Handle(OMSTradeData tradeMessage, IMessageHandlerContext context)
        {
            _context.CommitTrade(tradeMessage);

            // Commit to db
            Console.WriteLine(tradeMessage.Identifier);

            return Task.CompletedTask;

        }
    }
}
