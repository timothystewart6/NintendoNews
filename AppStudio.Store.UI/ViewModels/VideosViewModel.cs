using System;
using System.Windows;
using System.Windows.Input;

using AppStudio.Services;

namespace AppStudio.Data
{
    public class VideosViewModel : ViewModelBase<YouTubeSchema>
    {
        static private VideosViewModel _current = null;

        static public VideosViewModel Current
        {
            get { return _current ?? (_current = new VideosViewModel()); }
        }

        protected override string CacheKey
        {
            get { return "VideosDataSource"; }
        }

        protected override IDataSource<YouTubeSchema> CreateDataSource()
        {
            return new VideosDataSource(); // YouTubeDataSource
        }

        protected override void NavigateToSelectedItem()
        {
            DisableSelectionNavigation();
            // Not implemented in current version
        }

        public ICommand ShareTextCommand
        {
            get
            {
                return new RelayCommand<string>((fields) =>
                {
                    base.ShareItem("{Title}", "{Summary}", "{VideoUrl}", "{ImageUrl}");
                });
            }
        }

        public ICommand PinToStartCommand
        {
            get
            {
                return new RelayCommand<string>((fields) =>
                {
                    base.PinToStart("VideosViewDetail", "{Title}", "{Summary}", "{ImageUrl}");
                });
            }
        }

        public ICommand GoToSourceCommand
        {
            get
            {
                return new RelayCommand<string>((str) =>
                {
                    base.GoToSource("{ExternalUrl}");
                });
            }
        }
}
}
