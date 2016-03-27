using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CL.Javelin.Notification.Service.Startup))]
namespace CL.Javelin.Notification.Service
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HubConfiguration { EnableDetailedErrors = true };

            app
                .MapSignalR("/notifier", configuration)
                .UseNancy();
        }
    }
}
