using System;
using Microsoft.AspNet.SignalR;

namespace CL.Javelin.Notification.Service
{
    public class NotificationHub : Hub
    {
        public void Send(string name, string message)
        {
            Console.WriteLine("{0}", message);
            base.Clients.All.broadcastMessage(message);
        }
    }
}
