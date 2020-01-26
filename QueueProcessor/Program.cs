using NServiceBus;
using System;
using System.Threading.Tasks;
using TradeDataFeed.Contexts;

namespace QueueProcessor
{
    class Program
    {
        // Some of the messaging functions are a bit betwixt and between.
        // Namely when sending the messages from the API controller to the queue processor you’d possibly want to simplify
        // to just send the whole steam and have the logic of validity checking in one place. But if you plan on having more
        // endpoints and logic in the controller then it makes sense to split the messages out to be a whole message
        // and a specific trade message.

        static void Main(string[] args)
        {

            Console.Title = "Trade Data Queue Processing";

            // Configure EntityFramework
            MigrateDB();

            // Configure NServiceBus
            var endpointConfiguration = new EndpointConfiguration("TradeData");

            endpointConfiguration.UseTransport<LearningTransport>();

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");

// These extra error and metric functions don't seem to work in netcore anymore!?
            //            endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");
            //          var metrics = endpointConfiguration.EnableMetrics();
            //        metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));

            var endpointInstance = Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            endpointInstance.Stop()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

        }

        public static void MigrateDB()
        {
            var context = new TradeContext();
            context.Database.EnsureCreated();       // Doesn't seem autocreated in netcore?
        }

    }
}
