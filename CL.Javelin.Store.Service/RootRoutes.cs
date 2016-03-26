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
            var openFreightRequests = new List<Core.Domain.Freight.Request>
            {
                new Core.Domain.Freight.Request
                {
                    Customer = "Coca Cola",
                    Origin = "Detroit, MI",
                    Destination = "Chicago, IL",
                    Deadline = new DateTime(2016, 4, 1)
                }
            };

            return base.Response.AsJson(openFreightRequests);
        }
    }
}
