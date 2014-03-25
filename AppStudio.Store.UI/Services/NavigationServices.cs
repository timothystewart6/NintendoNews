using System;
using System.Net;

using Windows.System;

using AppStudio.Data;

namespace AppStudio.Services
{
    /// <summary>
    /// Not implementedted in current version 
    /// </summary>
    public class NavigationServices
    {
        static public void NavigateToPage(string path)
        {
            App.RootFrame.Navigate(Type.GetType("AppStudio." + path));
        }

        static public void NavigateTo(Uri uri)
        {
            // Not implemented
        }
    }
}
