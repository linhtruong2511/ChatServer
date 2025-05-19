using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.gui.event_
{
    internal class LoginSuccessEvent : EventArgs,IEvent
    {
        public string source { get; set; }
        public LoginSuccessEvent(string source)
        {
            this.source = source;
        }
        public string HandleEvent()
        {
            return source + " success  to login";
        }
    }
}
