using Common.Models;
using Newtonsoft.Json;
using NServiceBus;
using System;
using System.Text.RegularExpressions;
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

            if (tradeMessage.IsValid)
            {
                _context.CommitTrade(tradeMessage);
                var displayMessage = string.Format("Committing trade - {0}",
                    Regex.Replace(JsonConvert.SerializeObject(tradeMessage), @"\s+", ""));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(displayMessage);
                Console.ResetColor();

            }
            else
                ReportTradeError(tradeMessage);

            return Task.CompletedTask;

        }

        public Task ReportTradeError(OMSTradeData tradeMessage) {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid trade data - {0}", JsonConvert.SerializeObject(tradeMessage));
            Console.ResetColor();
            return Task.CompletedTask;
        }

    }
}
