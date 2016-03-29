using System;
using CL.Javelin.Services.Notification;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace CL.Javelin.Services.Notification
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HubConfiguration { EnableDetailedErrors = true };

            app
                .MapSignalR("/push", configuration)
                .UseNancy();

            Console.WriteLine("SignalR enabled, waiting for websocket connections");
        }
    }
}
