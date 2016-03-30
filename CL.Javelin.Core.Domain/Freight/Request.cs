using System;
using Newtonsoft.Json;

namespace CL.Javelin.Core.Domain.Freight
{
    public class Request
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        public string Customer { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public DateTime Deadline { get; set; } = DateTime.Today.Date;

        public bool Open { get; set; } = true;

        public int Weight { get; set; } = 0;

        public int Skids { get; set; } = 0;

        public int Pieces { get; set; } = 0;

        public string HazardClass { get; set; }
    }
}
