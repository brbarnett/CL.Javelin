using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Core;
using CL.Javelin.Core.Domain.Freight;
using CL.Javelin.Fulfillment.Client.Behaviors;
using CL.Javelin.Fulfillment.Client.Events.Freight.Request;
using Prism.Commands;
using Prism.Events;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace CL.Javelin.Fulfillment.Client.ViewModels
{
    public class BoardPageViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;

        private ObservableCollection<Request> _requests = new ObservableCollection<Request>();

        public ObservableCollection<Request> Requests
        {
            get { return this._requests; }
            private set { base.SetProperty(ref this._requests, value); }
        }

        private Request _selectedRequest;

        public Request SelectedRequest
        {
            get { return this._selectedRequest; }
            private set
            {
                base.SetProperty(ref this._selectedRequest, value);
                this.ToggleSelectedRequestOpenCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand ChangeSelectedRequestCommand { get; private set; }

        public DelegateCommand ToggleSelectedRequestOpenCommand { get; private set; }

        public BoardPageViewModel(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
            this._eventAggregator = eventAggregator;

            this.ChangeSelectedRequestCommand = new ActionCommand<Request>(request =>
            {
                this.SelectedRequest = request;                
            });

            this.ToggleSelectedRequestOpenCommand = new DelegateCommand(async () =>
            {
                if (this.SelectedRequest == null) return;

                this.SelectedRequest.Open = !this.SelectedRequest.Open;

                await Core.Utilities.Http.Put("http://127.0.0.1:9003/freight/requests", this.SelectedRequest);
            }, () => this.SelectedRequest != null);
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            await this.Load();
            this.ConnectToNotificationHub();
        }

        private async Task Load()
        {
            var requests = await Core.Utilities.Http.Get<IEnumerable<Request>>("http://127.0.0.1:9003/freight/requests");
            this.Requests = new ObservableCollection<Request>(requests);
        }

        private void ConnectToNotificationHub()
        {
            this._eventAggregator.GetEvent<FreightRequestCreated>().Subscribe(async (request) =>
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal, () =>
                    {
                        this.Requests.Add(request);
                    });
            });

            this._eventAggregator.GetEvent<FreightRequestUpdated>().Subscribe(async (request) =>
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal, () =>
                    {
                        var existing = this.Requests.SingleOrDefault(x => x.Id == request.Id);

                        if (existing == null) return;

                        int index = this.Requests.IndexOf(existing);
                        this.Requests[index] = request;
                    });
            });

            this._eventAggregator.GetEvent<FreightRequestDeleted>().Subscribe(async (request) =>
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal, () =>
                    {
                        var toRemove = this.Requests.SingleOrDefault(x => x.Id == request.Id);

                        if (toRemove == null) return;

                        this.Requests.Remove(toRemove);
                    });
            });
        }
    }
}