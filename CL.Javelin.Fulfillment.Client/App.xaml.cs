using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Notifications;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Logging;
using Prism.Windows;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;
using Windows.UI.Xaml;
using Microsoft.VisualBasic;
using Prism.Mvvm;
using Prism.Unity.Windows;

namespace CL.Javelin.Fulfillment.Client
{
    public sealed partial class App : PrismUnityApplication
    {
        // Bootstrap: App singleton service declarations
        //private TileUpdater _tileUpdater;

        public new IEventAggregator EventAggregator { get; set; }

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
            //SessionStateService.RegisterKnownType(typeof(Address));
            //SessionStateService.RegisterKnownType(typeof(PaymentMethod));
            //SessionStateService.RegisterKnownType(typeof(UserInfo));
            //SessionStateService.RegisterKnownType(typeof(CheckoutDataViewModel));
            //SessionStateService.RegisterKnownType(typeof(ObservableCollection<CheckoutDataViewModel>));
            //SessionStateService.RegisterKnownType(typeof(ShippingMethod));
            //SessionStateService.RegisterKnownType(typeof(Dictionary<string, Collection<string>>));
            //SessionStateService.RegisterKnownType(typeof(Order));
            //SessionStateService.RegisterKnownType(typeof(Product));
            //SessionStateService.RegisterKnownType(typeof(Collection<Product>));
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            this.EventAggregator = new EventAggregator();

            base.Container.RegisterInstance<INavigationService>(base.NavigationService);
            base.Container.RegisterInstance<ISessionStateService>(base.SessionStateService);
            base.Container.RegisterInstance<IEventAggregator>(base.EventAggregator);
            //Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));
            //Container.RegisterType<IAccountService, AccountService>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<ICredentialStore, RoamingCredentialStore>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<ICacheService, TemporaryFolderCacheService>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<ISecondaryTileService, SecondaryTileService>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<IAlertMessageService, AlertMessageService>(new ContainerControlledLifetimeManager());

            //// Register repositories
            //Container.RegisterType<IProductCatalogRepository, ProductCatalogRepository>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<IShoppingCartRepository, ShoppingCartRepository>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<ICheckoutDataRepository, CheckoutDataRepository>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<IOrderRepository, OrderRepository>(new ContainerControlledLifetimeManager());

            //// Register web service proxies
            //Container.RegisterType<IProductCatalogService, ProductCatalogServiceProxy>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<IOrderService, OrderServiceProxy>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<IShoppingCartService, ShoppingCartServiceProxy>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<IShippingMethodService, ShippingMethodServiceProxy>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<IIdentityService, IdentityServiceProxy>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<ILocationService, LocationServiceProxy>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<IAddressService, AddressServiceProxy>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<IPaymentMethodService, PaymentMethodServiceProxy>(new ContainerControlledLifetimeManager());

            //// Register child view models
            //Container.RegisterType<IShippingAddressUserControlViewModel, ShippingAddressUserControlViewModel>();
            //Container.RegisterType<IBillingAddressUserControlViewModel, BillingAddressUserControlViewModel>();
            //Container.RegisterType<IPaymentMethodUserControlViewModel, PaymentMethodUserControlViewModel>();
            //Container.RegisterType<ISignInUserControlViewModel, SignInUserControlViewModel>();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewModelTypeName = string.Format(CultureInfo.InvariantCulture, "AdventureWorks.UILogic.ViewModels.{0}ViewModel, AdventureWorks.UILogic, Version=1.1.0.0, Culture=neutral", viewType.Name);
                var viewModelType = Type.GetType(viewModelTypeName);
                if (viewModelType == null)
                {
                    viewModelTypeName = string.Format(CultureInfo.InvariantCulture, "AdventureWorks.UILogic.ViewModels.{0}ViewModel, AdventureWorks.UILogic.Windows, Version=1.0.0.0, Culture=neutral", viewType.Name);
                    viewModelType = Type.GetType(viewModelTypeName);
                }

                return viewModelType;
            });

            // Documentation on working with tiles can be found at http://go.microsoft.com/fwlink/?LinkID=288821&clcid=0x409
            //_tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
            //_tileUpdater.StartPeriodicUpdate(new Uri(Constants.ServerAddress + "/api/TileNotification"), PeriodicUpdateRecurrence.HalfHour);
            //var resourceLoader = Container.Resolve<IResourceLoader>();

            return base.OnInitializeAsync(args);
        }
    }
}
