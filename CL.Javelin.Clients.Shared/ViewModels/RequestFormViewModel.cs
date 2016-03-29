using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using CL.Javelin.Core.Domain.Freight;
using Prism.Commands;
using Prism.Windows.Mvvm;

namespace CL.Javelin.Clients.Shared.ViewModels
{
    public class RequestFormViewModel : ViewModelBase
    {
        private Request _request = new Request();

        public Request Request
        {
            get { return this._request; }
            private set
            {
                base.SetProperty(ref this._request, value);
                this.NotifyCommands();
            }
        }

        public string Customer
        {
            get { return this._request.Customer; }
            set
            {
                this._request.Customer = value;
                this.NotifyCommands();
            }
        }

        public ComboBoxItem Origin
        {
            set
            {
                if (value.Content == null) return;

                this._request.Origin = value.Content.ToString();
                this.NotifyCommands();
            }
        }

        public ComboBoxItem Destination
        {
            set
            {
                if (value.Content == null) return;

                this._request.Destination = value.Content.ToString();
                this.NotifyCommands();
            }
        }

        public DateTime Deadline
        {
            get { return this._request.Deadline; }
            set
            {
                this._request.Deadline = value;
                this.NotifyCommands();
            }
        }

        public bool Open
        {
            get { return this._request.Open; }
            set
            {
                this._request.Open = value;
                this.NotifyCommands();
            }
        }

        public ObservableCollection<ComboBoxItem> OriginLocations { get; private set; } = new ObservableCollection<ComboBoxItem>();

        public ObservableCollection<ComboBoxItem> DestinationLocations { get; private set; } = new ObservableCollection<ComboBoxItem>();

        public DateTime Today { get; } = DateTime.Today.Date;

        private readonly DelegateCommand[] _commandstoNotify;

        public RequestFormViewModel(DelegateCommand[] commandsToNotify)
        {
            this._commandstoNotify = commandsToNotify;
        }

        private void NotifyCommands()
        {
            foreach (DelegateCommand command in this._commandstoNotify)
            {
                command.RaiseCanExecuteChanged();
            }

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

        public bool IsValid()
        {
            if (this.Request == null) return false;
            if (String.IsNullOrEmpty(this.Request.Customer)) return false;
            if (String.IsNullOrEmpty(this.Request.Origin)) return false;
            if (String.IsNullOrEmpty(this.Request.Destination)) return false;

            return true;
        }

        public bool IsDirty()
        {
            return false;
        }

        public void Reset()
        {
            this.Request = new Request();
        }
    }
}
