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
    public partial class ImagesViewDetail
    {
        private NavigationHelper _navigationHelper;

        private ImagesViewModel _imagesViewModel = null;

        public ImagesViewDetail()
        {
            InitializeComponent();
            _navigationHelper = new NavigationHelper(this);
            DataContext = ImagesViewModel;
        }

        public ImagesViewModel ImagesViewModel
        {
            get { return _imagesViewModel ?? (_imagesViewModel = ImagesViewModel.Current); }
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
            await ImagesViewModel.LoadItems(true);
            DataContext = ImagesViewModel;
            base.OnNavigatedTo(e);
        }
    }
}
