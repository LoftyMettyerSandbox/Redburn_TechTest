using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TradeDataAPI
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.RegisterServices(Configuration);

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }


        //public IEndpointInstance GetNServiceBusEndpoint() {

        //    var endpointConfiguration = new EndpointConfiguration("ClientUI");

        //    var transport = endpointConfiguration.UseTransport<LearningTransport>();

        //    var routing = transport.Routing();
        //    routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");

        //    endpointConfiguration.SendFailedMessagesTo("error");
        //    endpointConfiguration.AuditProcessedMessagesTo("audit");
        //    endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");

        //    var metrics = endpointConfiguration.EnableMetrics();
        //    metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));

        //    _endpointInstance = await Endpoint.Start(endpointConfiguration)
        //        .ConfigureAwait(false);


        //}


    }
}
