using FontAwesome.Sharp;
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
    public partial class UCcontacts : UserControl
    {
        public UCcontacts()
        {
            InitializeComponent();
        }
        public IconChar avt { get => pic_Avt.IconChar; set => pic_Avt.IconChar = value; }
        public string username { get => lb_Username.Text; set => lb_Username.Text = value; }
        public string status { get => lb_Status.Text; set => lb_Status.Text = value; }
    }
}
