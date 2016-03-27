using System;
using Microsoft.AspNet.SignalR;
using Nancy;

namespace CL.Javelin.Notification.Service
{
    public class RootRoutes : NancyModule
    {
        private readonly NotificationHub _notificationHub;

        public RootRoutes()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            
            base.Post["/freight/requestCreated"] = async (x, ct) =>
            {
                Console.WriteLine("POST: /freight/requestCreated");

                hubContext.Clients.All.push("Name", "Message");

                return base.Response.AsJson(new { Success = true });
            };
        }
    }
}