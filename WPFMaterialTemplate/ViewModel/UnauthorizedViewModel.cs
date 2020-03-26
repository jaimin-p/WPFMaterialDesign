using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMaterialTemplate.ViewModel
{
    public class UnauthorizedViewModel :VmBase
    {
        public UnauthorizedViewModel()
        {
            UnauthorizedMessage = "Access Denied !!";
        }

        private string _unauthorizedMessage;
        public string UnauthorizedMessage
        {
            get { return _unauthorizedMessage; }
            set { _unauthorizedMessage = value; }
        }

        public new static bool IsUserAuthorized()
        {
            // If the user isn't authorized to view the unauthorized page CHAOS ENSUES
            return true;
        }
    }
}
