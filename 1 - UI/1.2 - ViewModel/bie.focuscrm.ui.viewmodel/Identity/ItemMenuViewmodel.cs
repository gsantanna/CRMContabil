using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bie.focuscrm.ui.viewmodel.Identity
{
    public class ItemMenuViewmodel
    {
        public string LinkText { get; set; }
        public string Group { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Roles { get; set; }
        public int Ordem { get; set; }
    }
}
