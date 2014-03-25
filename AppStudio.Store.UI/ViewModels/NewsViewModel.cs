using System;
using System.Windows;
using System.Windows.Input;

using AppStudio.Services;

namespace AppStudio.Data
{
    public class NewsViewModel : ViewModelBase<RssSchema>
    {
        static private NewsViewModel _current = null;

        static public NewsViewModel Current
        {
            get { return _current ?? (_current = new NewsViewModel()); }
        }

        protected override string CacheKey
        {
            get { return "NewsDataSource"; }
        }

        protected override IDataSource<RssSchema> CreateDataSource()
        {
            return new NewsDataSource(); // RssDataSource
        }

        protected override void NavigateToSelectedItem()
        {
            DisableSelectionNavigation();
            NavigationServices.NavigateToPage("NewsViewDetail");
        }

        public ICommand ShareTextCommand
        {
            get
            {
                return new RelayCommand<string>((fields) =>
                {
                    base.ShareItem("{Title}", "{Summary}", "{FeedUrl}", "{ImageUrl}");
                });
            }
        }

        public ICommand PinToStartCommand
        {
            get
            {
                return new RelayCommand<string>((fields) =>
                {
                    base.PinToStart("NewsViewDetail", "{Title}", "{Summary}", "{ImageUrl}");
                });
            }
        }

        public ICommand GoToSourceCommand
        {
            get
            {
                return new RelayCommand<string>((str) =>
                {
                    base.GoToSource("{FeedUrl}");
                });
            }
        }
}
}
