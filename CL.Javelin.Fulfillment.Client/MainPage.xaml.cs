using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CL.Javelin.Core.Domain.Freight;
using Microsoft.AspNet.SignalR.Client;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CL.Javelin.Fulfillment.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //public Request Request { get; set; } = new Request();
        public string Message { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            var hubConnection = new HubConnection("http://127.0.0.1:9002/push");
            IHubProxy notificationHubProxy = hubConnection.CreateHubProxy("NotificationHub");
            notificationHubProxy.On<string, string>("push", (n, m) => this.SetRequest(m));
            hubConnection.Start().Wait();
        }

        private void SetRequest(string message)
        {
            this.Message = message;
        }
    }
}
