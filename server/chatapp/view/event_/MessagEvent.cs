using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.gui.event_
{
    internal class MessageEvent : EventArgs,IEvent
    {
        public string source { get; set; }
        public string destination { get; set; }
        public MessageEvent(string source,string destination)
        {
            this.source = source;
            this.destination = destination;
        }
        public string HandleEvent()
        {
            return source + " send a message to " + destination;
        }
    }
}
