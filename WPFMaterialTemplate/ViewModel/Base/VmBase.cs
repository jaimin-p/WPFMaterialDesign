using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using MaterialDesignThemes.Wpf;
using System.Threading;
using System.Security.Principal;
using WPFMaterialTemplate.Properties;

namespace WPFMaterialTemplate.ViewModel
{
    public abstract class VmBase : ViewModelBase
    {


        public ICommand ChangeViewCommand { get; set; }

        public VmBase()
        {
            ChangeViewCommand = new RelayCommand<string>(ExecuteChangeViewCommand);
        }

        private void ExecuteChangeViewCommand(string viewName)
        {
            MessengerInstance.Send(new ChangeViewMessage { ViewName = viewName });
        }

        public static bool IsUserAuthorized(string viewId)
        {
            Thread.CurrentPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent() ?? new WindowsIdentity(null));

            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                if (viewId == ViewManager.AdminId)
                {
                    // Check if user if admin or not 
                    return false;
                }
                else
                {
                    // check other logic
                    return true;
                }
            }

            return false;
        }

        public static bool IsUserAdmin()
        {
            Thread.CurrentPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent() ?? new WindowsIdentity(null));

            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                // Admin Logic Here
                return true;
            }

            return false;
        }



        private SnackbarMessageQueue notificationq = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(500));
        public SnackbarMessageQueue NotificationQueue
        {
            get
            {
                return notificationq;
            }
            set
            {
                notificationq = value;
            }
        }


    }
}
