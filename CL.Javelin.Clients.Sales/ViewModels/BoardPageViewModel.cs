using CL.Javelin.Clients.Shared.ViewModels;
using CL.Javelin.Core.Domain.Freight;
using CL.Javelin.Core.Utilities;
using Prism.Commands;
using Prism.Events;

namespace CL.Javelin.Clients.Sales.ViewModels
{
    public class BoardPageViewModel : BoardPageViewModelBase
    {
        public DelegateCommand AddFreightRequestCommand { get; private set; }

        public BoardPageViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
            this.AddFreightRequestCommand = new DelegateCommand(async () =>
            {
                await Http.Post(this.ServiceUri, this.SelectedRequest.GetDomainRequest());
                this.SelectedRequest = null;
            }, () =>
            {
                return this.SelectedRequest.IsValid();
            });

            //this forces a blank request to be created
            this.SelectedRequest = null;
        }

        protected override string ServiceUri { get { return "http://127.0.0.1:9001/freight/requests"; } }

        protected override RequestFormViewModel CreateRequestFormViewModel(IRequest request)
        {
            return new RequestFormViewModel(request, new[] { this.AddFreightRequestCommand });
        }
    }
}
