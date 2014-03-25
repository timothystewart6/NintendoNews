using System;
using System.Windows;
using System.Windows.Input;

using AppStudio.Services;

namespace AppStudio.Data
{
    public class ImagesViewModel : ViewModelBase<FlickrSchema>
    {
        override protected string CacheKey
        {
            get { return "ImagesDataSource"; }
        }

        override protected IDataSource<FlickrSchema> CreateDataSource()
        {
            return new ImagesDataSource(); // FlickrDataSource
        }

        override public bool IsRefreshVisible
        {
            get { return ViewType == ViewTypes.List; }
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("ImagesViewDetail");
        }
    }
}
