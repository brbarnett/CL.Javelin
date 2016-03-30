﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using CL.Javelin.Clients.Shared.Events.Freight.Request;
using CL.Javelin.Core.Domain.Freight;
using Prism.Events;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace CL.Javelin.Clients.Shared.ViewModels
{
    public abstract class BoardPageViewModelBase : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;

        private ObservableCollection<Request> _requests = new ObservableCollection<Request>();

        public ObservableCollection<Request> Requests
        {
            get { return this._requests; }
            private set { base.SetProperty(ref this._requests, value); }
        }

        protected BoardPageViewModelBase(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException(nameof(eventAggregator));
            }
            this._eventAggregator = eventAggregator;
        }

        public sealed override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            await this.Load();
            this.ConnectToNotificationHub();
        }

        private async Task Load()
        {
            IEnumerable<Request> requests = await Core.Utilities.Http.Get<IEnumerable<Request>>(this.ServiceUri);
            this.Requests = new ObservableCollection<Request>(requests);
        }

        protected abstract string ServiceUri { get; }

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
                await DispatchAsync(
                    CoreDispatcherPriority.Normal, () =>
                    {
                        Request existing = this.Requests.SingleOrDefault(x => x.Id == request.Id);

                        if (existing == null) return;

                        int index = this.Requests.IndexOf(existing);
                        this.Requests[index] = request;
                    });
            });

            this._eventAggregator.GetEvent<FreightRequestDeleted>().Subscribe(async (request) =>
            {
                await DispatchAsync(
                    CoreDispatcherPriority.Normal, () =>
                    {
                        Request toRemove = this.Requests.SingleOrDefault(x => x.Id == request.Id);

                        if (toRemove == null) return;

                        this.Requests.Remove(toRemove);
                    });
            });
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected async Task<IAsyncAction> DispatchAsync(CoreDispatcherPriority priority, DispatchedHandler agileCallback)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                priority,
                agileCallback);
        }
    }
}