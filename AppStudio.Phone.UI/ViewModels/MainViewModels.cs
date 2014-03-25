using System.Threading.Tasks;
using System.Windows.Input;

using AppStudio.Data;
using AppStudio.Services;

namespace AppStudio
{
    public class MainViewModels : BindableBase
    {
       private AboutViewModel _aboutViewModel;
       private NewsViewModel _newsViewModel;
       private VideosViewModel _videosViewModel;
       private ImagesViewModel _imagesViewModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModels()
        {
            _selectedItem = AboutViewModel;
        }
 
        public AboutViewModel AboutViewModel
        {
            get { return _aboutViewModel ?? (_aboutViewModel = new AboutViewModel()); }
        }
 
        public NewsViewModel NewsViewModel
        {
            get { return _newsViewModel ?? (_newsViewModel = new NewsViewModel()); }
        }
 
        public VideosViewModel VideosViewModel
        {
            get { return _videosViewModel ?? (_videosViewModel = new VideosViewModel()); }
        }
 
        public ImagesViewModel ImagesViewModel
        {
            get { return _imagesViewModel ?? (_imagesViewModel = new ImagesViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            AboutViewModel.ViewType = viewType;
            NewsViewModel.ViewType = viewType;
            VideosViewModel.ViewType = viewType;
            ImagesViewModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public bool IsAppBarVisible
        {
            get
            {
                if (SelectedItem == null || SelectedItem == AboutViewModel)
                {
                    return true;
                }
                return SelectedItem != null ? SelectedItem.IsAppBarVisible : false;
            }
        }

        public bool IsLockScreenVisible
        {
            get { return SelectedItem == null || SelectedItem == AboutViewModel; }
        }

        public bool IsAboutVisible
        {
            get { return SelectedItem == null || SelectedItem == AboutViewModel; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("IsAppBarVisible");
            OnPropertyChanged("IsLockScreenVisible");
            OnPropertyChanged("IsAboutVisible");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadData(bool isNetworkAvailable)
        {
            var loadTasks = new Task[]
            { 
                AboutViewModel.LoadItems(isNetworkAvailable),
                NewsViewModel.LoadItems(isNetworkAvailable),
                VideosViewModel.LoadItems(isNetworkAvailable),
                ImagesViewModel.LoadItems(isNetworkAvailable),
            };
            await Task.WhenAll(loadTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand LockScreenCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    LockScreenServices.SetLockScreen("LockScreenImage.jpg");
                });
            }
        }
    }
}
