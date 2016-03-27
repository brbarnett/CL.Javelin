using System;
using Nancy;

namespace CL.Javelin.NotificationHub
{
    public class RootRoutes : NancyModule
    {
        public RootRoutes()
        {   
            base.Post["/freight/requestCreated", true] = async (x, ct) =>
            {
                Console.WriteLine("POST: /freight/requestCreated");

                return base.Response.AsJson(new { Success = true });
            };
        }
    }
}