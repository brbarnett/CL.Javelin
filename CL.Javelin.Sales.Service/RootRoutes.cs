using System;
using System.Threading;
using System.Threading.Tasks;
using Nancy;

namespace CL.Javelin.Sales.Service
{
    public class RootRoutes : NancyModule
    {
        public RootRoutes()
        {
            base.Post["/sales/createFreightRequest"] = this.CreateFreightRequest;
        }
        
        private async Task<dynamic> CreateFreightRequest(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine("POST: /freight/createRequest");

            var request = new Core.Domain.Freight.Request
            {
                Customer = "Coca Cola",
                Origin = "Atlanta, GA",
                Destination = "Chicago, IL",
                Deadline = new DateTime(2016, 4, 1)
            };

            // created, now notify
            await Core.Utilities.Http.Post("http://127.0.0.1:9000/freight/createRequest", request);

            return request;
        }
    }
}
