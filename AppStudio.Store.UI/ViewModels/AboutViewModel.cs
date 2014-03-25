using System;
using System.Windows;
using System.Windows.Input;

using AppStudio.Services;

namespace AppStudio.Data
{
    public class AboutViewModel : ViewModelBase<HtmlSchema>
    {
        static private AboutViewModel _current = null;

        static public AboutViewModel Current
        {
            get { return _current ?? (_current = new AboutViewModel()); }
        }

        protected override string CacheKey
        {
            get { return "AboutDataSource"; }
        }

        protected override IDataSource<HtmlSchema> CreateDataSource()
        {
            return new AboutDataSource(); // HtmlDataSource
        }

        public ICommand ShareTextCommand
        {
            get
            {
                return new RelayCommand<string>((fields) =>
                {
                    base.ShareItem("about", "{Content}", "", "");
                });
            }
        }

        public ICommand PinToStartCommand
        {
            get
            {
                return new RelayCommand<string>((fields) =>
                {
                    base.PinToStart("", "about", "{Content}", "");
                });
            }
        }
}
}
