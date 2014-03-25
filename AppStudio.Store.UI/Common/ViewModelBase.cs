using System;
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
                if (_selectedItem != null && _isSelectionNavigationEnabled)
                {
                    NotifyPropertyChanged("SelectedItem");
                    NavigateToSelectedItem();
                }
            }
        }

        protected BindableSchemaBase GetCurrentItem()
        {
            if (SelectedItem != null)
            {
                return SelectedItem;
            }
            if (Items != null && Items.Count > 0)
            {
                return Items[0];
            }
            return null;
        }

        public ICommand ImageSelectedCommand
        {
            get
            {
                return new RelayCommand<string>((src) =>
                {
                    if (!String.IsNullOrEmpty(src))
                    {
                        // Not implemented in current version
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
                        // Not implemented in current version
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
                    _kindOfDataLoaded = KindOfDataLoaded.Empty;
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
                // Not implemented in current version
            }
        }

        public void PinToStart(string path, string titleToShare, string messageToShare, string imageToShare)
        {
            // Not implemented in current version
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
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
