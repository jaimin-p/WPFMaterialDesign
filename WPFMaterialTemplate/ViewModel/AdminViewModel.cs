using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMaterialTemplate.ViewModel
{
    public class AdminViewModel :VmBase, INotifyPropertyChanged
    {
        public AdminViewModel()
        {

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
