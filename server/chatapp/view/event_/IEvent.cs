using chatapp.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.gui.event_
{
    public interface IEvent
    {
        string HandleEvent();
    }
}
