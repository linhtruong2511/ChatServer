using ClientApp.common.Class;
using ClientApp.utils;
using FontAwesome.Sharp;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace ClientApp
{
    public partial class MainForm : Form
    {
        public Context Context { get; set; }
        public Controller Controller { get; set; }
        public bool IsLoginSuccess { get; set; }
        public MainForm()
        {
            InitializeComponent();
            Context = new Context();
            Controller = new Controller(Context);
            LoginForm loginForm = new LoginForm(Context, this);
            flow_chat.Scroll += async (sender, e) =>
            {
                if (flow_chat.VerticalScroll.Value == VerticalScroll.Minimum)
                    NetworkUtils.Write(Context.Writer, new Packet(common.Enum.PacketTypeEnum.NUMBEROFCHATLOAD, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Context.lastChatLoad)), Context.MyId, Context.DestinationId));
            };
            flow_chat.MouseWheel += async (sender, e) =>
            {
                handleMouseWheel();
            };
            loginForm.Show();
            Task reading = Task.Run(() =>
            {
                while (!IsLoginSuccess) { }
                try
                {
                    Controller.HandleReadingTask(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while reading data from the server: " + ex.Message, "Error");
                    MessageBox.Show(ex.StackTrace);
                    //NetworkUtils.Write(Context.Writer, new Packet(common.Enum.PacketTypeEnum.DISCONNECT,Encoding.UTF8.GetBytes(""), Context.MyId, 0));
                }
            });

        }

        private void handleMouseWheel () 
        {
            if (flow_chat.VerticalScroll.Value == VerticalScroll.Minimum)
                NetworkUtils.Write(
                    Context.Writer, 
                    new Packet(
                        common.Enum.PacketTypeEnum.HISTORYCHAT,
                        Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Context.lastChatLoad)),
                        Context.MyId, Context.DestinationId));
        }
        public void ShowUser(UserInfo userInfo)
        {
            UCcontacts btn = new UCcontacts
            {
                Size = new Size(flow_UserList.Width, 112),
                avt = IconChar.UserCircle,
                username = userInfo.Name,
                //status = "Online"
            };
            //IconButton btn = new IconButton()
            //{
            //    Text = userInfo.Name,
            //    BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(25)))), ((int)(((byte)(47))))),
            //    FlatStyle = FlatStyle.Flat,
            //    ForeColor = Color.LightGray,
            //    IconChar = IconChar.UserCircle,
            //    IconSize = 80,
            //    IconColor = System.Drawing.SystemColors.GradientInactiveCaption,
            //    IconFont = IconFont.Solid,
            //    ImageAlign = ContentAlignment.MiddleLeft,
            //    Size = new Size(flow_UserList.Width, 100),
            //    Font = new System.Drawing.Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
            //    UseVisualStyleBackColor = false
            //};
            btn.Click += async (sender, e) =>
            {
                if (userInfo.Id != Context.DestinationId)
                {
                    lb_UserChat.Text = userInfo.Name;
                    lb_UserChat.Visible = true;
                    pic_User.Show();
                    flow_chat.Controls.Clear();
                    Context.Reset(userInfo.Id);
                    Console.WriteLine("/");
                    NetworkUtils.Write(
                        Context.Writer, new Packet(
                            common.Enum.PacketTypeEnum.HISTORYCHAT,
                            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Context.lastChatLoad)),
                            Context.MyId, Context.DestinationId));
                }
              
            };
            AddControlToPanel(flow_UserList, btn);
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            ((MainForm)sender).Hide();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkUtils.Write(Context.Writer, new Packet(common.Enum.PacketTypeEnum.DISCONNECT, Encoding.UTF8.GetBytes(""), Context.MyId, 0));
            Context.TcpClient.Dispose();
            Context.Writer.Dispose();
            Context.Reader.Dispose();
        }
        public void ShowUsers()
        {
            foreach (UserInfo userInfo in Context.Users)
            {
                if (userInfo.Id != Context.MyId)
                    ShowUser(userInfo);
            }
        }

        //private void btn_SendMessage_Click(object sender, EventArgs e)
        //{
        //    if (Context.DestinationId == 0)
        //    {
        //        MessageBox.Show("Please choice a destination user to communicate", "Notification");
        //    }
        //    else
        //    {
        //        if (txt_Message.Text != "")
        //        {
        //            Controller.SendMessage(txt_Message.Text, this);
        //            txt_Message.Text = "";
        //        }
        //    }
        //}

        //private void btn_SendFile_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog ofd_ChoiceFile = new OpenFileDialog
        //    {
        //        Title = "Chọn file cần gửi",
        //        Filter = "Hình ảnh|*.jpg|Hình ảnh|*.png|Hình ảnh|*.jpeg|text file|*.txt|word file|*.doc|excel file|*.xlsx|pdf file|*.pdf",
        //    };
        //    if (ofd_ChoiceFile.ShowDialog() == DialogResult.OK)
        //    {
        //        Controller.SendFile(ofd_ChoiceFile.FileName,this);
        //    }
        //}
        public void AddControlToPanel(Panel panel, Control control, bool IsPastObject = false)
        {
            if (panel.InvokeRequired)
            {
                panel.Invoke(new Action(() => panel.Controls.Add(control)));
            }
            else
            {
                panel.Controls.Add(control);
            }
            if (IsPastObject)
            {
                panel.Invoke(new Action(() => panel.Controls.SetChildIndex(control, 0)));
            }
        }
        public void ShowChatObject(ChatObject chatObject, bool IsPastObject)
        {
            

            int height = 0;
            if (chatObject is HistoryMessage)
            {
                ((Label)chatObject.Control).MaximumSize = new Size(flow_chat.Width / 2 + 50, 0);
                height = ((Label)(chatObject.Control)).Height;
            }
            else
            {
                MyFileInfo file = (MyFileInfo)chatObject;
                if (Controller.IsImageFile(file.fileName))
                {
                    ((PictureBox)chatObject.Control).Size = new Size(flow_chat.Width / 2 + 50, 200);
                    height = ((PictureBox)chatObject.Control).Height;
                }
                else
                {
                    ((Label)chatObject.Control).MaximumSize = new Size(flow_chat.Width / 2 + 50, 0);
                    height = ((Label)chatObject.Control).Height;
                }
            }
            FlowLayoutPanel flp = new FlowLayoutPanel
            {
                AutoSize = true,
                MinimumSize = new Size(flow_chat.Width - 40, 0),
                Padding = new Padding(10, 0, 0, 0)
            };
            IconPictureBox icon = new IconPictureBox
            {
                IconChar = IconChar.UserCircle,
                IconColor = System.Drawing.SystemColors.GradientActiveCaption,
                IconFont = IconFont.Solid,
                Size = new Size(60, 60),
                Padding = new Padding(5)
            };
            if (chatObject.Source == Context.MyId)
            {
                flp.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                flp.Controls.Add(icon);
                flp.FlowDirection = FlowDirection.LeftToRight;
            }
            ToolTip tooltip = new ToolTip
            {
                BackColor = Color.Aqua
            };
            this.Invoke(new Action(() =>
            {
                tooltip.SetToolTip(chatObject.Control, "Gửi lúc : " + chatObject.CreateAt.ToString("dd/MM/yyyy HH:mm:ss"));
            }));


            AddControlToPanel(flp, chatObject.Control);
            AddControlToPanel(flow_chat, flp, IsPastObject);
            flow_chat.ScrollControlIntoView(flp);
        }

        private void bt_Send_Click(object sender, EventArgs e)
        {
            if (Context.DestinationId == 0)
            {
                MessageBox.Show("Please choice a destination user to communicate", "Notification");
            }
            else
            {
                if (txt_Message.Text != "")
                {
                    Controller.SendMessage(txt_Message.Text, this);
                    txt_Message.Text = "";
                }
            }
        }

        private void bt_Gift_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd_ChoiceFile = new OpenFileDialog
            {
                Title = "Chọn file cần gửi",
                Filter = "Hình ảnh|*.jpg|Hình ảnh|*.png|Hình ảnh|*.jpeg|text file|*.txt|word file|*.doc|excel file|*.xlsx|pdf file|*.pdf",
            };
            if (ofd_ChoiceFile.ShowDialog() == DialogResult.OK)
            {
                Controller.SendFile(ofd_ChoiceFile.FileName, this);
            }
        }

        private void txt_Message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                if (Context.DestinationId == 0)
                {
                    MessageBox.Show("Please choice a destination user to communicate", "Notification");
                }
                else
                {
                    if (txt_Message.Text != "")
                    {
                        Controller.SendMessage(txt_Message.Text, this);
                        txt_Message.Text = "";
                    }
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
