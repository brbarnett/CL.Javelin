using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;

namespace CL.Javelin.Store.Service
{
    public class RootRoutes : NancyModule
    {
        public RootRoutes()
        {
            base.Get["/freight/openRequests", true] = this.OpenFreightRequests;
            base.Post["/freight/createRequest", true] = this.CreateFreightRequest;
        }

        private async Task<dynamic> OpenFreightRequests(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine("GET: /freight/openRequests");

            // mock data
            var openFreightRequests = new List<Core.Domain.Freight.Request>
            {
                new Core.Domain.Freight.Request
                {
                    Customer = "Coca Cola",
                    Origin = "Atlanta, GA",
                    Destination = "Chicago, IL",
                    Deadline = new DateTime(2016, 4, 1)
                },
                new Core.Domain.Freight.Request
                {
                    Customer = "McDonalds",
                    Origin = "Oakbrook, IL",
                    Destination = "Detroit, MI",
                    Deadline = new DateTime(2016, 4, 2)
                }
            };

            return base.Response.AsJson(openFreightRequests);
        }

        private async Task<dynamic> CreateFreightRequest(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine("POST: /freight/createRequest");

            var request = this.Bind<Core.Domain.Freight.Request>();

            // created, now notify
            await Core.Utilities.Http.Post("http://127.0.0.1:9002/freight/requestCreated", request);

            return request;
        }
    }
}
