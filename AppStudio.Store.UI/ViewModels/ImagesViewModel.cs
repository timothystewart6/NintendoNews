using System;
using System.Windows;
using System.Windows.Input;

using AppStudio.Services;

namespace AppStudio.Data
{
    public class ImagesViewModel : ViewModelBase<FlickrSchema>
    {
        static private ImagesViewModel _current = null;

        static public ImagesViewModel Current
        {
            get { return _current ?? (_current = new ImagesViewModel()); }
        }

        protected override string CacheKey
        {
            get { return "ImagesDataSource"; }
        }

        protected override IDataSource<FlickrSchema> CreateDataSource()
        {
            return new ImagesDataSource(); // FlickrDataSource
        }

        protected override void NavigateToSelectedItem()
        {
            DisableSelectionNavigation();
            NavigationServices.NavigateToPage("ImagesViewDetail");
        }

        public ICommand ShareTextCommand
        {
            get
            {
                return new RelayCommand<string>((fields) =>
                {
                    base.ShareItem("{Title}", "{Summary}", "{ImageUrl}", "{ImageUrl}");
                });
            }
        }

        public ICommand PinToStartCommand
        {
            get
            {
                return new RelayCommand<string>((fields) =>
                {
                    base.PinToStart("ImagesViewDetail", "{Title}", "{Summary}", "{ImageUrl}");
                });
            }
        }
}
}
