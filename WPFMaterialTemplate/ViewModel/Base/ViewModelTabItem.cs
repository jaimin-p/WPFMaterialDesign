using System;
using GalaSoft.MvvmLight;

namespace WPFMaterialTemplate.ViewModel.Base
{
    public class ViewModelTabItem: ObservableObject
    {
        public string Header { get; set; }

        public string Icon { get; set; }

        public string ViewId { get; set; }
        /// <summary>
        /// The viewmodel to use
        /// </summary>
        public Func<VmBase> GetViewModel
        {
            get { return () => ViewManager.GetViewModel(ViewId); }
        }
    }
}
