using System.Collections.Generic;
using System.Threading.Tasks;
using CL.Javelin.Core.Domain.Freight;

namespace CL.Javelin.Fulfillment.Client.DesignViewModels
{
    public class BoardPageDesignViewModel
    {
        public IEnumerable<Request> Requests { get; set; }

        public BoardPageDesignViewModel()
        {

            //var requests = await Core.Utilities.Http.Get<IEnumerable<Request>>("http://127.0.0.1:9003/fulfillment/getOpenRequests")
            //this.Requests = new ObservableCollection<Request>(requests); ;
            this.Load().RunSynchronously();
        }

        private async Task Load()
        {
            this.Requests = await Core.Utilities.Http.Get<IEnumerable<Request>>("http://127.0.0.1:9003/fulfillment/getOpenRequests");
        }
    }
}
