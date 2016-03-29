using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using CL.Javelin.Core.Domain.Freight;
using CL.Javelin.Sales.Client.Events.Freight.Request;
using Prism.Commands;
using Prism.Events;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace CL.Javelin.Sales.Client.ViewModels
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

        private Request _newRequest = new Request();

        public Request NewRequest
        {
            get { return this._newRequest; }
            private set
            {
                base.SetProperty(ref this._newRequest, value);
                this.AddFreightRequestCommand.RaiseCanExecuteChanged();
            }
        }

        public string Customer
        {
            get { return this._newRequest.Customer; }
            set
            {
                this._newRequest.Customer = value;
                this.AddFreightRequestCommand.RaiseCanExecuteChanged();
            }
        }

        public ComboBoxItem Origin
        {
            set
            {
                if (value.Content == null) return;

                this._newRequest.Origin = value.Content.ToString();
                this.AddFreightRequestCommand.RaiseCanExecuteChanged();
            }
        }

        public ComboBoxItem Destination
        {
            set
            {
                if (value.Content == null) return;

                this._newRequest.Destination = value.Content.ToString();
                this.AddFreightRequestCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTime Deadline
        {
            get { return this._newRequest.Deadline; }
            set
            {
                this._newRequest.Deadline = value;
                this.AddFreightRequestCommand.RaiseCanExecuteChanged();
            }
        }

        public bool Open
        {
            get { return this._newRequest.Open; }
            set
            {
                this._newRequest.Open = value;
                this.AddFreightRequestCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTime Today { get; } = DateTime.Today.Date;

        public ObservableCollection<ComboBoxItem> OriginLocations { get; private set; } = new ObservableCollection<ComboBoxItem>();
        public ObservableCollection<ComboBoxItem> DestinationLocations { get; private set; } = new ObservableCollection<ComboBoxItem>();

        public DelegateCommand AddFreightRequestCommand { get; private set; }

        public BoardPageViewModel(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
            this._eventAggregator = eventAggregator;

            this.AddFreightRequestCommand = new DelegateCommand(async () =>
            {
                await Core.Utilities.Http.Post("http://127.0.0.1:9001/freight/requests", this.NewRequest);
                this.NewRequest = new Request();
            }, () =>
            {
                if (this.NewRequest == null) return false;
                if (String.IsNullOrEmpty(this.NewRequest.Customer)) return false;
                if (String.IsNullOrEmpty(this.NewRequest.Origin)) return false;
                if (String.IsNullOrEmpty(this.NewRequest.Destination)) return false;
                
                return true;
            });

            // origin
            this.OriginLocations.Add(new ComboBoxItem { Content = "Chicago, IL" });
            this.OriginLocations.Add(new ComboBoxItem { Content = "Dallas, TX" });
            this.OriginLocations.Add(new ComboBoxItem { Content = "Denver, CO" });
            this.OriginLocations.Add(new ComboBoxItem { Content = "Los Angeles, CA" });
            this.OriginLocations.Add(new ComboBoxItem { Content = "New York, NY" });
            this.OriginLocations.Add(new ComboBoxItem { Content = "Philadelphia, PA" });
            this.OriginLocations.Add(new ComboBoxItem { Content = "Phoenix, AZ" });
            this.OriginLocations.Add(new ComboBoxItem { Content = "San Antonio, TX" });
            this.OriginLocations.Add(new ComboBoxItem { Content = "San Diego, CA" });

            // destination
            this.DestinationLocations.Add(new ComboBoxItem { Content = "Chicago, IL" });
            this.DestinationLocations.Add(new ComboBoxItem { Content = "Dallas, TX" });
            this.DestinationLocations.Add(new ComboBoxItem { Content = "Denver, CO" });
            this.DestinationLocations.Add(new ComboBoxItem { Content = "Los Angeles, CA" });
            this.DestinationLocations.Add(new ComboBoxItem { Content = "New York, NY" });
            this.DestinationLocations.Add(new ComboBoxItem { Content = "Philadelphia, PA" });
            this.DestinationLocations.Add(new ComboBoxItem { Content = "Phoenix, AZ" });
            this.DestinationLocations.Add(new ComboBoxItem { Content = "San Antonio, TX" });
            this.DestinationLocations.Add(new ComboBoxItem { Content = "San Diego, CA" });
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            await this.Load();
            this.ConnectToNotificationHub();
        }

        private async Task Load()
        {
            var requests = await Core.Utilities.Http.Get<IEnumerable<Request>>("http://127.0.0.1:9001/freight/requests");
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
