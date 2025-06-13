using chatapp.context;
using chatapp.view;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chatapp
{
    public partial class MainForm : Form
    {
        private readonly static List<string> Messages = new List<string>();
        UCDashboard UCDashboard;
        UCDepartment UCDepartment;
        UCUser UCUser;

        public MainForm()
        {
            App app = new App(this);
            UCDashboard = new UCDashboard();
            UCDepartment = new UCDepartment();
            UCUser = new UCUser();

            // style user controls
            UCUser.Dock = DockStyle.Fill;
            UCDepartment.Dock = DockStyle.Fill;
            UCDashboard.Dock = DockStyle.Fill;
            
            InitializeComponent();

            Task.Run(() => app.Start());
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            panel1.Controls.Add(new UCDashboard());
        }

        public void ShowAction(string Action)
        {
            UCDashboard.ShowAction(Action);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(UCDashboard);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(UCDepartment);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(UCUser);
        }
    }
}
