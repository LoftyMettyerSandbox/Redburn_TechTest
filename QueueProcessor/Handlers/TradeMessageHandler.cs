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
            
            var displayMessage = string.Format("Save message - {0}", Regex.Replace(message.Message, @"\s+", ""));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(displayMessage);
            Console.ResetColor();

            return Task.CompletedTask;
        }
    }
}
