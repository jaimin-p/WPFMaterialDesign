using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace WPFMaterialTemplate.ViewModel
{
    public static class ViewManager
    {
        #region View Identifiers
        // Add your view identifiers here
        // IDs must be unique!

        // Item Template Section
        // DO NOT REMOVE THE PLACEHOLDER COMMENT BELOW!!!
        public const string HomeId = "Home";
        public const string HomeTitle = "Home";

        public const string LiveChartId = "LiveChart";
        public const string LiveChartTitle = "Charts";

        public const string AdminId = "Admin";
        public const string AdminTitle = "Admin";

        public const string ThemeId = "Theme";
        public const string ThemeTitle = "Theme Settings";

        public const string UnauthorizedId = "Unauthorized";
        public const string UnauthorizedTitle = "Unauthorized Access";

  
        public const string AboutId = "About";
        public const string AboutTitle = "About";


        // [IdPlaceholder]
        #endregion

        public static readonly Dictionary<string, ViewWrapper> ViewTypes = new Dictionary<string, ViewWrapper>
        {
            // Item Template Section
            // DO NOT REMOVE THE PLACEHOLDER COMMENT BELOW!!!

            { HomeId, new ViewWrapper { Title = HomeTitle, VmType = typeof(HomeViewModel), GetViewModel = () => ServiceLocator.Current.GetInstance<HomeViewModel>()} },
            { LiveChartId, new ViewWrapper { Title = LiveChartTitle, VmType = typeof(LiveChartViewModel), GetViewModel = () => ServiceLocator.Current.GetInstance<LiveChartViewModel>()} },
            { UnauthorizedId, new ViewWrapper { Title = UnauthorizedTitle, VmType = typeof(UnauthorizedViewModel), GetViewModel = () => ServiceLocator.Current.GetInstance<UnauthorizedViewModel>()} },
            { AdminId, new ViewWrapper { Title = AdminTitle, VmType = typeof(AdminViewModel), GetViewModel = () => ServiceLocator.Current.GetInstance<AdminViewModel>()} }

			// [DictPlaceholder]
        };


        public static bool IsUserAuthorized(string viewId)
        {
            ViewWrapper wrapper;

            if (ViewTypes.TryGetValue(viewId, out wrapper))
            {
                MethodInfo m = wrapper.VmType.GetMethod("IsUserAuthorized", BindingFlags.Public | BindingFlags.Static);

                if (m != null)
                {
                    return (bool)m.Invoke(null, null);
                }
            }

            return VmBase.IsUserAuthorized(viewId);
        }

        public class ViewWrapper
        {
            internal string Title { get; set; }
            internal string Description { get; set; }
            internal Type VmType { get; set; }
            internal Func<VmBase> GetViewModel { get; set; }
        }

        public static VmBase GetViewModel(string viewId)
        {
            ViewWrapper wrapper;
            if (ViewTypes.TryGetValue(viewId, out wrapper))
            {
                return wrapper.GetViewModel();
            }

            return ServiceLocator.Current.GetInstance<HomeViewModel>();
        }
    }
}