using System.Windows.Input;
using CL.Javelin.Clients.Shared.Behaviors;
using CL.Javelin.Clients.Shared.ViewModels;
using CL.Javelin.Core.Domain.Freight;
using CL.Javelin.Core.Utilities;
using Prism.Commands;
using Prism.Events;

namespace CL.Javelin.Clients.Fulfillment.ViewModels
{
    public class BoardPageViewModel : BoardPageViewModelBase
    {
        public ICommand ChangeSelectedRequestCommand { get; private set; }

        public DelegateCommand ToggleSelectedRequestOpenCommand { get; private set; }

        public DelegateCommand DeleteSelectedRequestCommand { get; private set; }

        public BoardPageViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
            this.ChangeSelectedRequestCommand = new ActionCommand<Request>(request =>
            {
                this.SelectedRequest.SetRequest(request);            
            });

            this.ToggleSelectedRequestOpenCommand = new DelegateCommand(this.ToggleSelectedRequestOpen, this.CanOperateOnRequest);
            this.DeleteSelectedRequestCommand = new DelegateCommand(this.DeleteSelectedRequest, this.CanOperateOnRequest);

            //this forces a blank request to be created
            this.SelectedRequest = null;
        }

        private async void ToggleSelectedRequestOpen()
        {
            if (!this.SelectedRequest.IsValid()) return;

            this.SelectedRequest.Open = !this.SelectedRequest.Open;

            await Http.Put(this.ServiceUri, this.SelectedRequest.GetDomainRequest());

            this.SelectedRequest = null;
        }

        private async void DeleteSelectedRequest()
        {
            if (!this.SelectedRequest.IsValid()) return;

            this.SelectedRequest.Open = !this.SelectedRequest.Open;

            await Http.Delete(this.ServiceUri +  $"/{this.SelectedRequest.GetDomainRequest().Id}");

            this.SelectedRequest = null;
        }

        private bool CanOperateOnRequest()
        {
            return this.SelectedRequest.IsValid();
        }

        protected override string ServiceUri { get { return "http://127.0.0.1:9003/freight/requests"; } }

        protected override RequestFormViewModel CreateRequestFormViewModel(IRequest request)
        {
            return new RequestFormViewModel(request, new[] { this.ToggleSelectedRequestOpenCommand, this.DeleteSelectedRequestCommand });
        }    
    }
}