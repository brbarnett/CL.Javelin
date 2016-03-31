using System;
using System.Windows.Input;
using CL.Javelin.Clients.Shared.ViewModels;
using Prism.Events;

namespace CL.Javelin.Clients.Fulfillment.ViewModels
{
    public class BoardPageViewModel : BoardPageViewModelBase
    {
        public ICommand ToggleSelectedRequestOpenCommand { get; private set; }

        public ICommand DeleteSelectedRequestCommand { get; private set; }

        public BoardPageViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
        }

        protected override string ServiceUri { get { return "http://127.0.0.1:9003/freight/requests"; } }
    }
}