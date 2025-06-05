using System;
using System.Collections.Generic;
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
        }
        public void ShowAction(string Action)
        {
            textBox1.Text += DateTime.Now.ToString() + " : " + Action + "\r\n";
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
