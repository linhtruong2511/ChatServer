using chatapp.context;
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
        private static List<string> messages = new List<string>();

        public MainForm()
        {
            App app = new App(this);
            InitializeComponent();

            Task.Run(() => app.Start());
        }
        public void ShowAction(string Action)
        {
            //textBox1.Text += DateTime.Now.ToString() + " : " + Action + "\r\n";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(16, 16);
            imageList.Images.Add("user", Properties.Resources.bar_chart);

            this.btnDashboard.ImageList = imageList;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
