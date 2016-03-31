using System;
using Windows.UI.Xaml;

namespace CL.Javelin.Clients.Shared.ViewModels
{
    public interface IResourcesAwareViewModel
    {
        ResourceDictionary Resources { get; set; }
    }
}
