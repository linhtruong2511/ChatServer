using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.gui.event_
{
    internal class SignUpFailEvent : EventArgs,IEvent
    {
        public string source;
        public SignUpFailEvent(string source)
        {
            this.source = source;
        }
        public string HandleEvent()
        {
            return source + " sign up fail";
        }
    }
}
