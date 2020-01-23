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
