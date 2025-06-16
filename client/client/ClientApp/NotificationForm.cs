using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public class NotificationForm : Form
    {
        public Label label { get; set; }
        public NotificationForm(string message, int x, int y)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.BackColor = Color.DarkSlateBlue;
            this.ForeColor = Color.White;
            this.Size = new Size(300, 60);
            this.TopMost = true;

            label = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Text = message,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            this.Controls.Add(label);
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(x - 10, y - 10);
            Timer timer = new Timer { Interval = 3000 };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                this.Close();
            };
            timer.Start();
        }
    }
}
