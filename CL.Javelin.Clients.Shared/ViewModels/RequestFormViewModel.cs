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
        private RequestViewModel _request;

        public RequestViewModel Request
        {
            get
            {
                if (ReferenceEquals(this._request, null))
                {
                    SetProperty(ref this._request, new RequestViewModel(this.NotifyCommands));
                    this.NotifyCommands();
                }
                return this._request;
            }
            set
            {
                SetProperty(ref this._request, value);
                this.NotifyCommands();
            }
        }

        public ComboBoxItem Origin
        {
            set
            {
                if (value.Content == null) return;

                this.Request.Origin = value.Content.ToString();
            }
        }

        public ComboBoxItem Destination
        {
            set
            {
                if (value.Content == null) return;

                this.Request.Destination = value.Content.ToString();
            }
        }

        public ComboBoxItem HazardClass
        {
            set
            {
                if (value.Content == null) return;

                this.Request.HazardClass = value.Content.ToString();
            }
        }

        public bool Open
        {
            get { return this.Request.Open; }
            set
            {
                this.Request.Open = value;
            }
        }

        public ObservableCollection<ComboBoxItem> OriginLocations { get; private set; } = new ObservableCollection<ComboBoxItem>();

        public ObservableCollection<ComboBoxItem> DestinationLocations { get; private set; } = new ObservableCollection<ComboBoxItem>();

        public ObservableCollection<ComboBoxItem> HazardClasses { get; private set; } = new ObservableCollection<ComboBoxItem>();

        public DateTime Today { get; } = DateTime.Today.Date;

        private readonly DelegateCommand[] _commandstoNotify;

        public RequestFormViewModel(DelegateCommand[] commandsToNotify)
        {
            this._commandstoNotify = commandsToNotify;

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

            // hazard classes
            this.HazardClasses.Add(new ComboBoxItem { Content = "1.1: Explosives with a mass explosion hazard" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "1.2: Explosives with a projection hazard" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "1.3: Explosives with predominantly a fire hazard" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "1.4: Explosives with no significant blast hazard" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "1.5: Very insensitive explosives" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "1.6: Extremely insensitive explosive articles" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "2.1: Flammable gases" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "2.2: Non-flammable gases" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "2.3: Poison gases" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "2.4: Corrosive gases" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "3.1: Flashpoint below -18°C(0°F)" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "3.2: Flashpoint below -18°C and above, but less than 23°C(73°F)" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "3.3: Flashpoint 23°C and up to 61°C(141°F)" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "4.1: Flammable solids" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "4.2: Spontaneously combustible materials" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "4.3: Materials that are dangerous when wet" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "5.1: Oxidizers" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "5.2: Organic peroxides" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "6.1: Poisonous materials" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "6.2: Etiologic(infectious) materials" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "7: Radioactive material" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "8: Corrosives" });
            this.HazardClasses.Add(new ComboBoxItem { Content = "9: Miscellaneous dangerous substances and articles" });
        }

        private void NotifyCommands()
        {
            foreach (DelegateCommand command in this._commandstoNotify)
            {
                command.RaiseCanExecuteChanged();
            }
        }

        public bool IsValid()
        {
            if (ReferenceEquals(this._request, null)) return false;
            if (String.IsNullOrEmpty(this._request.Customer)) return false;
            if (String.IsNullOrEmpty(this._request.Origin)) return false;
            if (String.IsNullOrEmpty(this._request.Destination)) return false;

            return true;
        }

        public bool IsDirty()
        {
            return false;
        }

        public void Reset()
        {
            new AbstractRequestCopier().Copy(null, this._request); //reset to default values
            this.NotifyCommands();
        }

        public void SetRequest(Request request)
        {
            this.Request = new RequestViewModel(this.NotifyCommands, request);
        }

        public Request GetRequest()
        {
            return new Request(this._request);
        }
    }
}
