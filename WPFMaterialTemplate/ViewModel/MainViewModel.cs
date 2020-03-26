using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WPFMaterialTemplate.ViewModel.Base;

namespace WPFMaterialTemplate.ViewModel
{
  
    public class MainViewModel : VmBase ,INotifyPropertyChanged
    {
        public MainViewModel()
        {
            MessengerInstance.Register<ChangeViewMessage>(this, ReceiveChangeViewMessage);

            SetInitialViewCommand = new RelayCommand(ExecuteSetInitialViewCommand);
            OnPreviewMouseLeftButtonUp = new RelayCommand(ExecuteOnPreviewMouseLeftButtonUp);

            ViewTabs.Add( CreateItem(ViewManager.AdminTitle, ViewManager.AdminId,"Lock" ));
            ViewTabs.Add(CreateItem(ViewManager.HomeTitle, ViewManager.HomeId , "Home"));
            ViewTabs.Add(CreateItem(ViewManager.LiveChartTitle, ViewManager.LiveChartId, "film"));

            NotificationQueue.Enqueue("Welcome to Material Desgin !!");
        }


        private static ViewModelTabItem CreateItem(string header,string viewid,string icon)
        {
            ViewModelTabItem item = new ViewModelTabItem();

            item.Header = header;
            item.ViewId = viewid;
            item.Icon = icon;
     
            return item;
        }


        public ICommand SetInitialViewCommand { get; set; }
        public ICommand OnPreviewMouseLeftButtonUp { get; set; }


        private ObservableCollection<ViewModelTabItem> _viewTabs = new ObservableCollection<ViewModelTabItem>();
        public ObservableCollection<ViewModelTabItem> ViewTabs
        {
            get { return _viewTabs; }
            set { Set(() => ViewTabs, ref _viewTabs, value); }
        }

        private ViewModelTabItem _selectedTab;
        public ViewModelTabItem SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                Set(() => SelectedTab, ref _selectedTab, value);
                if (SelectedTab != null && CurrentViewModelId != SelectedTab.ViewId)
                {
                    ChangeView(SelectedTab.ViewId);
                }
            }
        }

        private VmBase _currentViewModel;
        public VmBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                Set(() => CurrentViewModel, ref _currentViewModel, value);
                var tab = ViewTabs.FirstOrDefault(x => x.ViewId == CurrentViewModelId);

                if (tab != null)
                {
                    _selectedTab = tab;
                }
                else if (tab == null)
                {
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        SelectedTab = null;
                    }), DispatcherPriority.Send, null);
                }
                NotifyPropertyChanged("CurrentViewModel");
                NotifyPropertyChanged("SelectedTab");
            }
        }

        private string _currentViewModelId;
        public string CurrentViewModelId
        {
            get { return _currentViewModelId; }
            set { Set(() => CurrentViewModelId, ref _currentViewModelId, value); }
        }


        private void ExecuteSetInitialViewCommand()
        {
            ChangeView(ViewManager.HomeId);
        }

        private bool _istogglechecked = false;
        public bool IsToggleChecked
        {
            get
            {
                return _istogglechecked;
            }
            set
            {
                _istogglechecked = value;
                NotifyPropertyChanged("IsToggleChecked");
            }
        }

        private void ExecuteOnPreviewMouseLeftButtonUp()
        {
            _istogglechecked = false;
            NotifyPropertyChanged("IsToggleChecked");
        }

        private void ReceiveChangeViewMessage(ChangeViewMessage message)
        {
            if (message != null)
            {
                ChangeView(message.ViewName);
            }
        }


        public string CurrentUser
        {
            get
            {
                return Environment.UserName;
            }
        }

        private void ChangeView(string viewId)
        {
            // Check if User is authorized to view 
            if (!ViewManager.IsUserAuthorized(viewId))
            {
                CurrentViewModel = ViewManager.GetViewModel(ViewManager.UnauthorizedId);
                return;
            }

            CurrentViewModelId = viewId;
            CurrentViewModel = ViewManager.GetViewModel(viewId);
           
        }

        #region Notify
        new public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string Obj)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(Obj));
            }
        }
        #endregion
    }
}