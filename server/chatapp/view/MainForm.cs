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
        UCProject UCProject;
        UCAttendance UCAttendance;
        UCSalary UCSalary;
        UCTask UCTask;
        public MainForm()
        {
            App app = new App(this);
            InitializeComponent();


            UCDashboard = new UCDashboard();
            UCDepartment = new UCDepartment(this.panel1);
            UCUser = new UCUser();
            UCProject = new UCProject();
            UCAttendance = new UCAttendance();
            UCTask = new UCTask();
            UCSalary = new UCSalary();
            
            // style user controls
            UCSalary.Dock = DockStyle.Fill;
            UCTask.Dock = DockStyle.Fill;
            UCAttendance.Dock = DockStyle.Fill;
            UCUser.Dock = DockStyle.Fill;
            UCDepartment.Dock = DockStyle.Fill;
            UCDashboard.Dock = DockStyle.Fill;
            UCProject.Dock = DockStyle.Fill;

            Task.Run(() => app.Start());
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            panel1.Controls.Add(UCDashboard);
        }

        public void ShowAction(string Action)
        {
            Console.WriteLine($"[MainForm.ShowAction] {Action} | ThreadId: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
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

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(UCProject);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(UCAttendance);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(UCSalary);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(UCTask);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
