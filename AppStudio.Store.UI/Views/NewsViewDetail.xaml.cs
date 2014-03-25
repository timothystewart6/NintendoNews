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
    public partial class NewsViewDetail
    {
        private NavigationHelper _navigationHelper;

        private NewsViewModel _newsViewModel = null;

        public NewsViewDetail()
        {
            InitializeComponent();
            _navigationHelper = new NavigationHelper(this);
            DataContext = NewsViewModel;
        }

        public NewsViewModel NewsViewModel
        {
            get { return _newsViewModel ?? (_newsViewModel = NewsViewModel.Current); }
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
            await NewsViewModel.LoadItems(true);
            DataContext = NewsViewModel;
            base.OnNavigatedTo(e);
        }
    }
}
