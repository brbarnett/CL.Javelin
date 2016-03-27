using Microsoft.AspNet.SignalR;

namespace CL.Javelin.Notification.Service
{
    public class NotificationHub : Hub
    {
        public void Send(string name, string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.push(name, message);
        }
    }
}
