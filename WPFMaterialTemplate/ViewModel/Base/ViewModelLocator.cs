/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WPFMaterialTemplate"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using WPFMaterialTemplate.ViewModel;

namespace WPFMaterialTemplate.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public partial class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<LiveChartViewModel>();
            SimpleIoc.Default.Register<UnauthorizedViewModel>();
            SimpleIoc.Default.Register<AdminViewModel>();


        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public HomeViewModel Home => ServiceLocator.Current.GetInstance<HomeViewModel>();
        public LiveChartViewModel LiveChart => ServiceLocator.Current.GetInstance<LiveChartViewModel>();
        public UnauthorizedViewModel Unauthorized => ServiceLocator.Current.GetInstance<UnauthorizedViewModel>();
        public AdminViewModel Admin => ServiceLocator.Current.GetInstance<AdminViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}