using System;
using System.Collections.Generic;
using Nancy;

namespace CL.Javelin.Store.Service
{
    public class RootRoutes : NancyModule
    {
        public RootRoutes()
        {
            base.Get["/freight/openRequests"] = this.OpenFreightRequests;
        }

        private dynamic OpenFreightRequests(dynamic parameters)
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
    }
}
