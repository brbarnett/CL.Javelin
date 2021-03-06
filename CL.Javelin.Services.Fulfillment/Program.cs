﻿using System;
using Nancy.Hosting.Self;

namespace CL.Javelin.Services.Fulfillment
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://127.0.0.1:9003";

            using (var host = new NancyHost(new Uri(url)))
            {
                host.Start();
                Console.WriteLine($"Fulfillment server listening on {url}");
                Console.ReadLine();
            }
        }
    }
}
