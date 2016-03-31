using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Nancy;
using Nancy.ModelBinding;

namespace CL.Javelin.Services.Notification.Modules.Freight
{
    public class RequestsModule : NancyModule
    {
        private readonly IHubContext _notificationHubContext;

        private const string BaseUrl = "/freight/requests";

        public RequestsModule()
        {
            this._notificationHubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();

            base.Post[$"{BaseUrl}/created"] = this.RequestCreated;
            base.Post[$"{BaseUrl}/updated"] = this.RequestUpdated;
            base.Post[$"{BaseUrl}/deleted"] = this.RequestDeleted;
        }

        private async Task<dynamic> RequestCreated(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            var request = this.Bind<Core.Domain.Freight.Request>();

            this._notificationHubContext.Clients.All.FreightRequestCreated(request);

            return base.Response.AsJson(new { Success = true });
        }

        private async Task<dynamic> RequestUpdated(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            var request = this.Bind<Core.Domain.Freight.Request>();

            this._notificationHubContext.Clients.All.FreightRequestUpdated(request);

            return base.Response.AsJson(new { Success = true });
        }

        private async Task<dynamic> RequestDeleted(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            var request = this.Bind<Core.Domain.Freight.Request>();

            this._notificationHubContext.Clients.All.FreightRequestDeleted(request);

            return base.Response.AsJson(new { Success = true });
        }
    }
}