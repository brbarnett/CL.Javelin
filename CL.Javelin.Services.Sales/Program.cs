using System;
using Nancy.Hosting.Self;

namespace CL.Javelin.Services.Sales
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://127.0.0.1:9001";

            using (var host = new NancyHost(new Uri(url)))
            {
                host.Start();
                Console.WriteLine($"Sales server listening on {url}");
                Console.ReadLine();
            }
        }
    }
}
