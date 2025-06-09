using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp.common.Class
{
    public abstract class ChatObject
    {
        public bool IsDeleted { get; set; } = false;
        public Control Control { get; set; }
        public int Source { get; set; }
        public DateTime CreateAt { get; set; }    
    }
}
