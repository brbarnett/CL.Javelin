using System;
using Nancy.Hosting.Self;

namespace CL.Javelin.Services.Store
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://127.0.0.1:9000";

            using (var host = new NancyHost(new Uri(url)))
            {
                host.Start();
                Console.WriteLine($"Store server listening on {url}");
                Console.ReadLine();
            }
        }
    }
}
