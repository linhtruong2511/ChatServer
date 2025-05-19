using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client.gui
{
    public partial class Gui : Form
    {
        public App mainapp { get; set; }
        public Gui()
        {
            InitializeComponent();
        }
        public void Run()
        {
            Task.Run(() => mainapp.Start());
            mainapp.clientevent += HandleClientEvent;
        }
        public void HandleClientEvent(object sender,EventArgs e)
        {

        }

        private void Gui_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Label mymessage = new Label();
            //mymessage.Text = textBox1.Text;
            //mymessage.TextAlign = ContentAlignment.MiddleRight;
            
        }
    }
}
