using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CL.Javelin.Core.Utilities;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;

namespace CL.Javelin.Sales.Service.Routes.Freight
{
    public class RequestRoutes : NancyModule
    {
        private const string BaseUrl = "/freight/requests";

        public RequestRoutes()
        {
            base.Get[$"{BaseUrl}"] = this.GetFreightRequests;
            base.Post[$"{BaseUrl}"] = this.CreateFreightRequest;
        }

        private async Task<dynamic> GetFreightRequests(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            IEnumerable<Core.Domain.Freight.Request> requests = 
                await Http.Get<IEnumerable<Core.Domain.Freight.Request>>("http://127.0.0.1:9000/freight/requests");

            return base.Response.AsJson(requests);
        }

        private async Task<dynamic> CreateFreightRequest(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            var request = this.Bind<Core.Domain.Freight.Request>();

            // post to Store for creation
            var response = await Http.Post<Core.Domain.Freight.Request>("http://127.0.0.1:9000/freight/requests", request);
            
            return base.Response.AsJson(response.Content);
        }
    }
}
