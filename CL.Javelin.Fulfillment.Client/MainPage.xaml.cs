using Windows.UI.Xaml.Controls;

namespace CL.Javelin.Fulfillment.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //var requests = await Core.Utilities.Http.Get<IEnumerable<Request>>("http://127.0.0.1:9003/fulfillment/getOpenRequests")
            //this.Requests = new ObservableCollection<Request>(requests); ;
            //this.Requests.Add();

            //var hubConnection = new HubConnection("http://127.0.0.1:9002/push");
            //IHubProxy notificationHubProxy = hubConnection.CreateHubProxy("NotificationHub");
            //notificationHubProxy.On<string, string>("push", (n, m) => this.SetRequest(m));
            //hubConnection.Start().Wait();
        }
    }
}
