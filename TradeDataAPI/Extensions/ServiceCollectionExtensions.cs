using Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(
        this IServiceCollection services, IConfiguration configuration)
    {

        // NServiceBus
        var endpointConfiguration = new EndpointConfiguration("TradeDataAPI");
        var transport = endpointConfiguration.UseTransport<LearningTransport>();

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(OMSTradeData), "TradeData");

        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.AuditProcessedMessagesTo("audit");
                    
        //  endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");
        //  var metrics = endpointConfiguration.EnableMetrics();
        //  metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));

        var endpoint = Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();
        services.AddSingleton(endpoint);

        return services;
    }

}