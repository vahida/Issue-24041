using System;
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BareBoneMembershipApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpClient("PongClient", client =>
                {
                    client.BaseAddress = new Uri(Configuration["PongBaseUrl"]);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    if (bool.Parse(Configuration["HttpClient_Http2Enabled"])) client.DefaultRequestVersion = new Version(2, 0);
                    if (bool.Parse(Configuration["HttpClient_InfiniteTimeSpanEnabled"])) client.Timeout = Timeout.InfiniteTimeSpan;
                })
                .SetHandlerLifetime(TimeSpan.FromSeconds(int.Parse(Configuration["HttpClient_HandlerLifeTimeInSeconds"])))
                .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler()
                { 
                    PooledConnectionIdleTimeout = TimeSpan.FromSeconds(int.Parse(Configuration["SocketsHandler_PooledConnection_IdleTimeout_InSeconds"])),
                    PooledConnectionLifetime = bool.Parse(Configuration["SocketsHandler_PooledConnection_Lifetime_Enabled"])
                        ? TimeSpan.FromSeconds(int.Parse(Configuration["SocketsHandler_PooledConnection_Lifetime_InSeconds"])) : Timeout.InfiniteTimeSpan, 
                    MaxConnectionsPerServer = bool.Parse(Configuration["SocketsHandler_MaxConnectionsPerServer_Enabled"])
                        ? int.Parse(Configuration["SocketsHandler_MaxConnectionsPerServer"]) : int.MaxValue
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
