using System.Collections.Generic;
using System.Threading.Tasks;
using CL.Javelin.Core.Domain.Freight;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace CL.Javelin.Fulfillment.Client.ViewModels
{
    public class BoardPageViewModel : ViewModelBase
    {
        private IReadOnlyCollection<Request> _requests; 
        public IReadOnlyCollection<Request> Requests
        {
            get { return this._requests; }
            private set { base.SetProperty(ref this._requests, value); }
        }

        public BoardPageViewModel() { }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            await this.Load();
        }

        private async Task Load()
        {
            this.Requests = await Core.Utilities.Http.Get<IReadOnlyCollection<Request>>("http://127.0.0.1:9003/fulfillment/getOpenRequests");
        }
    }
}
