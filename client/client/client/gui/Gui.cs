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
            Task.Run(() => mainapp.Start(this));
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
            Message mymessage = new Message(message.Contents, mainapp.MyID, 2, 0);
            ShowMyMessage(mymessage);
            flp_messagescreen.FlowDirection = FlowDirection.TopDown;
            flp_messagescreen.WrapContents = false;
            flp_messagescreen.AutoScroll = true;
            textBox1.Text = "";
        }
        public void ShowMyMessage(Message myMessage)
        {
            Label lbl = new Label();
            lbl.Text = myMessage.Contents;
            lbl.TextAlign = ContentAlignment.MiddleRight;
            lbl.MaximumSize = new Size(flp_messagescreen.Width - 50, 0);
            lbl.Size = new Size(flp_messagescreen.Width - 50, 40);
            if (flp_messagescreen.InvokeRequired)
            {
                flp_messagescreen.Invoke(new MethodInvoker(() => flp_messagescreen.Controls.Add(lbl)));// đưa về luồng chính rồi mới có thể thay đổi được controls
            }
            else
            {
                flp_messagescreen.Controls.Add(lbl);
            }
        }
        public void ShowOtherMessage(Message otherMesssage)
        {
            Label lbl = new Label();
            lbl.Text = otherMesssage.Contents;
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.MaximumSize = new Size(flp_messagescreen.Width - 50, 0);
            lbl.Size = new Size(flp_messagescreen.Width - 50, 40);
            if (flp_messagescreen.InvokeRequired)
            {
                flp_messagescreen.Invoke(new MethodInvoker(() => flp_messagescreen.Controls.Add(lbl)));
            }
            else
            {
                flp_messagescreen.Controls.Add(lbl);
            }
        }
        public void ShowMessages(List<Message> messages)
        {
            for(int i = 0; i < messages.Count; i++)
            {
                if (messages[i].Source != mainapp.MyID)
                {
                    ShowOtherMessage(messages[i]);
                }
                else
                {
                    ShowMyMessage(messages[i]);
                }
            }
        } 
        //public void flp_messagescreen_Paint()
        //{

        //}
    }
}
