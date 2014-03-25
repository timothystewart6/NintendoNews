using System;
using System.Windows;
using System.Windows.Input;

using AppStudio.Services;

namespace AppStudio.Data
{
    public class NewsViewModel : ViewModelBase<RssSchema>
    {
        override protected string CacheKey
        {
            get { return "NewsDataSource"; }
        }

        override protected IDataSource<RssSchema> CreateDataSource()
        {
            return new NewsDataSource(); // RssDataSource
        }

        override public bool IsGoToSourceVisible
        {
            get { return ViewType == ViewTypes.Detail; }
        }
        
        override public void GoToSource()
        {
            base.GoToSource("{FeedUrl}");
        }

        override public bool IsRefreshVisible
        {
            get { return ViewType == ViewTypes.List; }
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("NewsViewDetail");
        }
    }
}
