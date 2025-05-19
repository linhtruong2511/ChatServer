using chatapp.gui.event_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chatapp
{
    public partial class MainForm : Form
    {
        private static List<string> messages = new List<string>();

        public MainForm()
        {
            InitializeComponent();
            App app = new App(this);
            Task.Run(() => app.Start());
            app.serverEvent += HandleServerEvent;



        }
        public void HandleServerEvent(object sender,EventArgs e)
        {
           
            //if(e is IEvent)
            //{
            //    txt_notification.Text += DateTime.Now.ToString() + " : " + ((IEvent)e).HandleEvent() + "\r\n";
            //}
        }

        public void AddMessage(string message)
        {
            messages.Add(message);
            loadDashboard();
        }

        public void loadDashboard()
        {
           
        }




        private void dgvDashboard_DataSourceChanged(object sender, EventArgs e)
        {

        }
    }
}
