using Common.Models;
using NServiceBus;
using System;
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

            Console.WriteLine(message.Message);

            return Task.CompletedTask;
        }
    }
}
