using CL.Javelin.Clients.Shared.ViewModels;
using Prism.Events;

namespace CL.Javelin.Clients.Sales.ViewModels
{
    public class BoardPageViewModel : BoardPageViewModelBase
    {
        public BoardPageViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
        }

        protected override string ServiceUri { get { return "http://127.0.0.1:9001/freight/requests"; } }
    }
}
