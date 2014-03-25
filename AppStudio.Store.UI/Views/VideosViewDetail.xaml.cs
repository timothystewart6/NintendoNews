using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

using AppStudio.Data;
using AppStudio.Common;

namespace AppStudio
{
    public partial class VideosViewDetail
    {
        private NavigationHelper _navigationHelper;

        private VideosViewModel _videosViewModel = null;

        public VideosViewDetail()
        {
            InitializeComponent();
            _navigationHelper = new NavigationHelper(this);
            DataContext = VideosViewModel;
        }

        public VideosViewModel VideosViewModel
        {
            get { return _videosViewModel ?? (_videosViewModel = VideosViewModel.Current); }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this._navigationHelper; }
        }

        override protected async void OnNavigatedTo(NavigationEventArgs e)
        {
            await VideosViewModel.LoadItems(true);
            DataContext = VideosViewModel;
            base.OnNavigatedTo(e);
        }
    }
}
