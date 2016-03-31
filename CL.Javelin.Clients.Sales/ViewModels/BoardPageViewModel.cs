using System;
using CL.Javelin.Clients.Shared.ViewModels;
using CL.Javelin.Core.Domain.Freight;
using CL.Javelin.Core.Utilities;
using Prism.Commands;
using Prism.Events;

namespace CL.Javelin.Clients.Sales.ViewModels
{
    public class BoardPageViewModel : BoardPageViewModelBase
    {
        public BoardPageViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
        }

        protected override string ServiceUri { get { return "http://127.0.0.1:9001/freight/requests"; } }
    }
}
