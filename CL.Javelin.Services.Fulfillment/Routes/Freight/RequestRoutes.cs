using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CL.Javelin.Core.Utilities;
using Nancy;
using Nancy.ModelBinding;

namespace CL.Javelin.Services.Fulfillment.Routes.Freight
{
    public class RequestRoutes : NancyModule
    {
        private const string BaseUrl = "/freight/requests";

        public RequestRoutes()
        {
            base.Get[$"{BaseUrl}"] = this.GetFreightRequests;
            base.Put[$"{BaseUrl}"] = this.UpdateFreightRequest;
            base.Delete[$"{BaseUrl}/{{id}}"] = this.DeleteFreightRequest;
        }

        private async Task<dynamic> GetFreightRequests(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            // post to Store for creation
            IEnumerable<Core.Domain.Freight.Request> requests =
                await Http.Get<IEnumerable<Core.Domain.Freight.Request>>("http://127.0.0.1:9000/freight/requests");

            return base.Response.AsJson(requests);
        }

        private async Task<dynamic> UpdateFreightRequest(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            var request = this.Bind<Core.Domain.Freight.Request>();

            // post to Store for creation
            var response = await Http.Put<Core.Domain.Freight.Request>("http://127.0.0.1:9000/freight/requests", request);

            return base.Response.AsJson(response.Content);
        }

        private async Task<dynamic> DeleteFreightRequest(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            string id = parameters.id;

            // post to Store for creation
            var response = await Http.Delete($"http://127.0.0.1:9000/freight/requests/{id}");

            return base.Response.AsJson(response.Content);
        }
    }
}