using System;

namespace CL.Javelin.Core.Domain.Freight
{
    public sealed class AbstractRequestCopier : IAbstractCopier<IRequest>
    {
        public void Copy(IRequest source, IRequest destination)
        {
            if (!ReferenceEquals(destination, null))
            {
                if (ReferenceEquals(source, null))
                {
                    destination.Id = default(Guid);
                    destination.Customer = default(string);
                    destination.Origin = default(string);
                    destination.Destination = default(string);
                    destination.Deadline = DateTime.Today.Date;
                    destination.Open = true;
                    destination.Weight = 0;
                    destination.Skids = 0;
                    destination.Pieces = 0;
                    destination.HazardClass = default(string);
                }
                else
                {
                    destination.Id = source.Id;
                    destination.Customer = source.Customer;
                    destination.Origin = source.Origin;
                    destination.Destination = source.Destination;
                    destination.Deadline = source.Deadline;
                    destination.Open = source.Open;
                    destination.Weight = source.Weight;
                    destination.Skids = source.Skids;
                    destination.Pieces = source.Pieces;
                    destination.HazardClass = source.HazardClass;
                }
            }
        }
    }
}
