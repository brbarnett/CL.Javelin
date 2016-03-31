using System;
using System.Collections.Generic;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CL.Javelin.Clients.Shared.ViewModels;

namespace CL.Javelin.Clients.Shared.Views
{
    public static class ResourceExtensions
    {
        public static void SetResourcesAwareRegistration(this Page page, bool register)
        {
            if (!ReferenceEquals(page, null))
            {
                if (register)
                {
                    page.DataContextChanged += OnDataContextChanged;
                    if (!ReferenceEquals(page.DataContext, null))
                    {
                        OnDataContextChanged(page, page.DataContext);
                    }
                }
                else
                {
                    page.DataContextChanged -= OnDataContextChanged;
                }
            }
        }

        private static void OnDataContextChanged(FrameworkElement sender, object newDataContext)
        {
            IResourcesAwareViewModel resourcesAwareViewModel = newDataContext as IResourcesAwareViewModel;
            if (!ReferenceEquals(resourcesAwareViewModel, null))
            {
                ResourceDictionary resources = sender.Resources;
                resourcesAwareViewModel.Resources = resources;
            }
        }
    }
}
