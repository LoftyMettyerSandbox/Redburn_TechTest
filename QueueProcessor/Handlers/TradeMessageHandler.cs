using Common.Models;
using NServiceBus;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TradeDataFeed.Contexts;

namespace QueueProcessor.Handlers
{

    public class TradeMessageHandler : IHandleMessages<TradeMessage>
    {

        TradeContext _context;
        public TradeMessageHandler()
        {
            _context = new TradeContext();
        }

        public Task Handle(TradeMessage message, IMessageHandlerContext context)
        {
            _context.CommitMessage(message);
            
            var displayMessage = Regex.Replace(message.Message, @"\s+", "");
            Console.WriteLine(displayMessage);

            return Task.CompletedTask;
        }
    }
}
