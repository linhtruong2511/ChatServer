using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chatapp.gui.event_
{
    internal class ServerOnlineEvent : EventArgs,IEvent
    {
        public string HandleEvent()
        {
            return "Server is running";
        }
    }
}
