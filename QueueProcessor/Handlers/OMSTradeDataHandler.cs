using Common.Models;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace TradeDataAPI.Handlers
{
    public class OMSTradeDataHandler : IHandleMessages<OMSTradeData>
    {
        public Task Handle(OMSTradeData message, IMessageHandlerContext context)
        {

            // Commit to db
            var count = 0;
            Console.WriteLine(message.Identifier);

            return Task.CompletedTask;

        }
    }
}
