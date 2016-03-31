using System;

namespace CL.Javelin.Core.Domain.Freight
{
    public interface IRequest
    {
        Guid Id { get; set; }

        string Customer { get; set; }

        string Origin { get; set; }

        string Destination { get; set; }

        DateTimeOffset? Deadline { get; set; }

        bool Open { get; set; }

        int Weight { get; set; }

        int Skids { get; set; }

        int Pieces { get; set; }

        string HazardClass { get; set; }
    }
}
