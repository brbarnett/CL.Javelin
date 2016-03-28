using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CL.Javelin.Core.Utilities;
using Nancy;
using Nancy.ModelBinding;

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

            // post to Store for creation
            IEnumerable<Core.Domain.Freight.Request> requests = 
                await Http.Get<IEnumerable<Core.Domain.Freight.Request>>("http://127.0.0.1:9000/freight/requests");

            return requests;
        }

        private async Task<dynamic> CreateFreightRequest(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            var request = this.Bind<Core.Domain.Freight.Request>();

            // post to Store for creation
            var response = await Http.Post<Core.Domain.Freight.Request>("http://127.0.0.1:9000/freight/requests", request);
            
            return response.Content;
        }
    }
}
