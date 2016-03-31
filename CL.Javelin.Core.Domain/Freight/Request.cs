using System;
using Newtonsoft.Json;

namespace CL.Javelin.Core.Domain.Freight
{
    public class Request : IRequest
    {
        public Request()
            : this(null)
        {
        }

        public Request(IRequest request)
        {
            new AbstractRequestCopier().Copy(request, this);
        }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        public string Customer { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public DateTimeOffset? Deadline { get; set; }

        public bool Open { get; set; }

        public int Weight { get; set; }

        public int Skids { get; set; }

        public int Pieces { get; set; }

        public string HazardClass { get; set; }
    }
}
