using CL.Javelin.Clients.Shared.ViewModels;
using CL.Javelin.Core.Utilities;
using Prism.Commands;
using Prism.Events;

namespace CL.Javelin.Clients.Sales.ViewModels
{
    public class BoardPageViewModel : BoardPageViewModelBase
    {
        public RequestFormViewModel NewRequest { get; set; }
        
        public DelegateCommand AddFreightRequestCommand { get; private set; }

        public BoardPageViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
            this.AddFreightRequestCommand = new DelegateCommand(async () =>
            {
                await Http.Post(this.ServiceUri, this.NewRequest.GetRequest());
                this.NewRequest.Reset();
            }, () =>
            {
                return this.NewRequest.IsValid();
            });

            this.NewRequest = new RequestFormViewModel(new[] {this.AddFreightRequestCommand});
        }

        protected override string ServiceUri { get { return "http://127.0.0.1:9001/freight/requests"; } }
    }
}
