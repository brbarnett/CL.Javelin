using System;
using Nancy.Hosting.Self;

namespace CL.Javelin.NotificationHub
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://127.0.0.1:9002";

            using (var host = new NancyHost(new Uri(url)))
            {
                host.Start();
                Console.WriteLine($"NotificationHub server listening on {url}");
                Console.ReadLine();
            }
        }
    }
}
