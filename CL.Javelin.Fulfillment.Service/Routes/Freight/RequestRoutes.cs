using System;
using System.Collections.Generic;
using Nancy;

namespace CL.Javelin.Fulfillment.Service.Routes.Freight
{
    public class RequestRoutes : NancyModule
    {
        public RequestRoutes()
        {   
            base.Get["/fulfillment/getOpenRequests"] = async (x, ct) =>
            {
                Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

                var openFreightRequests = await Core.Utilities.Http.Get<List<Core.Domain.Freight.Request>>("http://127.0.0.1:9000/freight/requests/open");

                return base.Response.AsJson(openFreightRequests);
            };
        }
    }
}