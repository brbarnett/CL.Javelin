﻿using System;
using System.Globalization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;
using Windows.UI.Xaml;
using CL.Javelin.Core.Domain.Freight;
using CL.Javelin.Fulfillment.Client.Events.Freight.Request;
using Microsoft.AspNet.SignalR.Client;
using Prism.Mvvm;
using Prism.Unity.Windows;

namespace CL.Javelin.Fulfillment.Client
{
    public sealed partial class App : PrismUnityApplication
    {
        public new IEventAggregator EventAggregator { get; set; }

        public App()
        {
            this.InitializeComponent();
        }

        // Documentation on navigation between pages is at http://go.microsoft.com/fwlink/?LinkID=288815&clcid=0x409
        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            if (args != null && !string.IsNullOrEmpty(args.Arguments))
            {
                // The app was launched from a Secondary Tile
                // Navigate to the item's page
                base.NavigationService.Navigate("ItemDetail", args.Arguments);
            }
            else
            {
                // Navigate to the initial page
                base.NavigationService.Navigate("Board", null);
            }

            Window.Current.Activate();
            return Task.FromResult<object>(null);
        }

        protected override void OnRegisterKnownTypesForSerialization()
        {
            // Set up the list of known types for the SuspensionManager
            base.SessionStateService.RegisterKnownType(typeof(Request));
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            this.EventAggregator = new EventAggregator();

            base.Container.RegisterInstance<INavigationService>(base.NavigationService);
            base.Container.RegisterInstance<ISessionStateService>(base.SessionStateService);
            base.Container.RegisterInstance<IEventAggregator>(base.EventAggregator);
          
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewModelTypeName = string.Format(CultureInfo.InvariantCulture, "CL.Javelin.Fulfillment.Client.ViewModels.{0}ViewModel", viewType.Name);
                var viewModelType = Type.GetType(viewModelTypeName);

                return viewModelType;
            });
            
            HubConnection hubConnection = new HubConnection("http://127.0.0.1:9002/push");
            IHubProxy notificationHubProxy = hubConnection.CreateHubProxy("NotificationHub");
            notificationHubProxy.On<Request>("FreightRequestCreated", (obj) =>
            {
                base.EventAggregator.GetEvent<FreightRequestCreated>().Publish(obj);
            });
            notificationHubProxy.On<Request>("FreightRequestUpdated", (obj) =>
            {
                base.EventAggregator.GetEvent<FreightRequestUpdated>().Publish(obj);
            });
            notificationHubProxy.On<Request>("FreightRequestDeleted", (obj) =>
            {
                base.EventAggregator.GetEvent<FreightRequestDeleted>().Publish(obj);
            });
            hubConnection.Start().Wait();

            return base.OnInitializeAsync(args);
        }
    }
}
