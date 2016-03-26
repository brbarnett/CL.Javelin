using System.Collections.Generic;
using Nancy;

namespace CL.Javelin.Fulfillment.Service
{
    public class RootRoutes : NancyModule
    {
        public RootRoutes()
        {   
            base.Get["/fulfillment/getOpenRequests", true] = async (x, ct) =>
            {
                var openFreightRequests = await Core.Utilities.Http.Get<List<Core.Domain.Freight.Request>>("http://127.0.0.1:9000/freight/openRequests");

                return base.Response.AsJson(openFreightRequests);
            };
        }
    }
}