using System;

namespace CL.Javelin.Core.Domain.Freight
{
    public class Request
    {
        public string Customer { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public DateTime Deadline { get; set; }
    }
}
