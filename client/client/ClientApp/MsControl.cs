using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class MsControl : UserControl
    {
        public MsControl()
        {
            InitializeComponent();
        }
        public string text { get => lb_message.Text; set => lb_message.Text = value; }
        public string time { get => lb_time.Text; set => lb_time.Text = value; }

    }
}
