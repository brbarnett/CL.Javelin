using System;
using Microsoft.Owin.Hosting;

namespace CL.Javelin.Notification.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://127.0.0.1:9002";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"NotificationHub server listening on {url}");
                Console.ReadLine();
            }
        }
    }
}
