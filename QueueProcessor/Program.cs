using NServiceBus;
using System;
using System.Threading.Tasks;

namespace QueueProcessor
{
    class Program
    {

        static void Main(string[] args) { 

            Console.Title = "Trade Data Queue Processing";

            var endpointConfiguration = new EndpointConfiguration("TradeData");

            endpointConfiguration.UseTransport<LearningTransport>();

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
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

    }
}
