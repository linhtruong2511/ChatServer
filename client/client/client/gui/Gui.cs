using client.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
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

        public void button1_Click(object sender, EventArgs e)
        {
            
            SendMessageRequest message = new SendMessageRequest();
            message.Contents = textBox1.Text;
            MessageService.SendMessage(mainapp.writer, message);

            Label lbl = new Label();
            lbl.Text = textBox1.Text;
            lbl.TextAlign = ContentAlignment.MiddleRight;
            lbl.MaximumSize = new Size(flp_messagescreen.Width - 50, 0);

            lbl.Size = new Size(flp_messagescreen.Width - 50, 40);
            flp_messagescreen.FlowDirection = FlowDirection.TopDown;
            flp_messagescreen.WrapContents = false;
            flp_messagescreen.AutoScroll = true;
            flp_messagescreen.Controls.Add(lbl);
            flp_messagescreen.ScrollControlIntoView(lbl);
            textBox1.Text = "";
        }
        public void ReceiveMessage()
        {

        } 

        private void flp_messagescreen_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
