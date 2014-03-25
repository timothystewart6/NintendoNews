using System;
using System.Net;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

using AppStudio.Data;
using AppStudio.Services;

namespace AppStudio
{
    public interface IViewModel
    {
        Task Refresh();
    }

    abstract public class ViewModelBase<T> : INotifyPropertyChanged, IViewModel where T : BindableSchemaBase
    {
        private enum KindOfDataLoaded
        {
            Empty,
            FromCache,
            FromDataSource
        }

        public event PropertyChangedEventHandler PropertyChanged = null;

        private bool _isSelectionNavigationEnabled = false;

        private KindOfDataLoaded _kindOfDataLoaded = KindOfDataLoaded.Empty;

        protected ObservableCollection<BindableSchemaBase> _items = null;
        protected BindableSchemaBase _selectedItem = null;

        protected IDataSource<T> _dataSource;

        public IDataSource<T> DataSource
        {
            get { return _dataSource ?? (_dataSource = CreateDataSource()); }
        }

        public ObservableCollection<BindableSchemaBase> Items
        {
            get { return _items; }
            set
            {
                var oldValue = _items;
                _items = value;
                if (_items != null && _items != oldValue)
                {
                    NotifyPropertyChanged("Items");
                }
            }
        }

        public BindableSchemaBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged("SelectedItem");
                if (_selectedItem != null && _isSelectionNavigationEnabled)
                {
                    NavigateToSelectedItem();
                }
            }
        }

        public ICommand ImageSelectedCommand
        {
            get
            {
                return new RelayCommand<string>((src) =>
                {
                    if (!String.IsNullOrEmpty(src))
                    {
                        NavigationServices.NavigateToPage("ImageViewer", "url=" + HttpUtility.UrlEncode(src));
                    }
                });
            }
        }

        public ICommand TextToSpeechCommand
        {
            get
            {
                return new RelayCommand<string>((fields) =>
                {
                    if (!String.IsNullOrEmpty(fields))
                    {
                        var currentItem = SelectedItem ?? Items[0];
                        SpeechServices.TextToSpeech(currentItem.GetValues(fields));
                    }
                });
            }
        }

        public void EnableSelectionNavigation()
        {
            SelectedItem = null;
            _isSelectionNavigationEnabled = true;
        }

        public void DisableSelectionNavigation()
        {
            _isSelectionNavigationEnabled = false;
        }

        public async Task LoadItems(bool isNetworkAvailable)
        {
            if (_kindOfDataLoaded == KindOfDataLoaded.Empty)
            {
                // Read items from Cache
                var records = await AppCache.GetItemsAsync<T>(CacheKey);
                if (records != null)
                {
                    _kindOfDataLoaded = KindOfDataLoaded.FromCache;
                    Items = new ObservableCollection<BindableSchemaBase>(records);
                }
            }

            if (_kindOfDataLoaded != KindOfDataLoaded.FromDataSource && isNetworkAvailable)
            {
                // Read items from DataSource
                var records = await DataSource.LoadData();
                if (records != null)
                {
                    _kindOfDataLoaded = KindOfDataLoaded.FromDataSource;
                    Items = new ObservableCollection<BindableSchemaBase>(records);
                    await AppCache.AddItemsAsync(CacheKey, records);
                }
            }
        }

        public async Task LoadItem(string id)
        {
            T item = await AppCache.GetItemAsync<T>(id);
            if (item != null)
            {
                if (Items == null)
                {
                    Items = new ObservableCollection<BindableSchemaBase>(new T[] { item });
                    _selectedItem = item;
                }
                else
                {
                    Items.Clear();
                    Items.Add(item);
                    NotifyPropertyChanged("Items");
                    _selectedItem = item;
                    NotifyPropertyChanged("SelectedItem");
                }
            }
        }

        public async Task Refresh()
        {
            // Read items from DataSource
            var records = await DataSource.Refresh();
            if (records != null)
            {
                _kindOfDataLoaded = KindOfDataLoaded.FromDataSource;
                Items = new ObservableCollection<BindableSchemaBase>(records);
                await AppCache.AddItemsAsync(CacheKey, records);
            }
        }

        public void ShareItem(string titleToShare, string messageToShare, string linkToShare, string imageToShare)
        {
            var currentItem = SelectedItem ?? Items[0];
            if (currentItem != null)
            {
                var shareServices = new ShareServices();
                shareServices.Share(currentItem.GetValue(titleToShare), currentItem.GetValue(messageToShare), currentItem.GetValue(linkToShare), currentItem.GetValue(imageToShare));
            }
        }

        public void PinToStart(string path, string titleToShare, string messageToShare, string imageToShare)
        {
            var currentItem = SelectedItem ?? Items[0];
            if (currentItem != null)
            {
                var tileInfo = new TileServices.TileInfo()
                {
                    Id = currentItem.Id,
                    Title = currentItem.GetValue(titleToShare).Truncate(128),
                    BackTitle = currentItem.GetValue(titleToShare).Truncate(128),
                    BackContent = currentItem.GetValue(messageToShare).Truncate(128),
                    BackgroundImagePath = currentItem.GetValue(imageToShare),
                    BackBackgroundImagePath = currentItem.GetValue(imageToShare),
                    Count = 0
                };
                TileServices.PinTostart(path, tileInfo, SelectedItem);
            }
        }

        public void GoToSource(string field)
        {
            var currentItem = SelectedItem ?? Items[0];
            if (currentItem != null)
            {
                string url = currentItem.GetValue(field);
                if (!String.IsNullOrEmpty(url) && Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    NavigationServices.NavigateTo(new Uri(url, UriKind.Absolute));
                }
            }
        }

        virtual protected void NavigateToSelectedItem() { }

        abstract protected string CacheKey { get; }
        abstract protected IDataSource<T> CreateDataSource();

        #region NotifyPropertyChanged
        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                try
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
                catch { }
            }
        }
        #endregion
    }
}
