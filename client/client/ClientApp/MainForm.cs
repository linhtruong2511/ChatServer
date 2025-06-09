using ClientApp.common.Class;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            flp_MessageScreen.Scroll += async (sender, e) =>
            {
                if (flp_MessageScreen.VerticalScroll.Value == VerticalScroll.Minimum)
                   await Controller.LoadConservation(this);
            };
            flp_MessageScreen.MouseWheel += async (sender, e) =>
            {
                if (flp_MessageScreen.VerticalScroll.Value == VerticalScroll.Minimum)
                    await Controller.LoadConservation(this);
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
                }
            });

        }
        public void ShowUser(UserInfo userInfo)
        {
            Button btn = new Button()
            {
                Text = userInfo.Name,
                Size = new Size(flp_UsersScreen.Width - 10, 40),
            };
            btn.Click += async (sender, e) =>
            {
                if (userInfo.Id != Context.DestinationId)
                {
                    flp_MessageScreen.Controls.Clear();
                    Context.Reset(userInfo.Id);
                    await Controller.LoadConservation(this);
                }
            };
            AddControlToPanel(flp_UsersScreen, btn);
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            ((MainForm)sender).Hide();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
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

        private void btn_SendMessage_Click(object sender, EventArgs e)
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

        private void btn_SendFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd_ChoiceFile = new OpenFileDialog
            {
                Title = "Chọn file cần gửi",
                Filter = "Hình ảnh|*.jpg|Hình ảnh|*.png|Hình ảnh|*.jpeg|text file|*.txt|word file|*.doc|excel file|*.xlsx|pdf file|*.pdf",
            };
            if (ofd_ChoiceFile.ShowDialog() == DialogResult.OK)
            {
                Controller.SendFile(ofd_ChoiceFile.FileName,this);
            }
        }
        public void AddControlToPanel(Panel panel,Control control,bool IsPastObject = false)
        {
            if (panel.InvokeRequired)
            {
                panel.Invoke(new MethodInvoker(() => panel.Controls.Add(control)));
            }
            else
            {
                panel.Controls.Add(control);
            }
            if (IsPastObject)
            {
                panel.Controls.SetChildIndex(control, 0);
            }
        }
        public void ShowChatObject(ChatObject chatObject,bool IsPastObject)
        {
            int height = 0;
            if (chatObject is HistoryMessage)
            {
                ((Label)chatObject.Control).MaximumSize = new Size(flp_MessageScreen.Width / 2 + 50, 0);
                height = ((Label)(chatObject.Control)).PreferredHeight;
            }
            else
            {
                MyFileInfo file = (MyFileInfo)chatObject;
                if (Controller.IsImageFile(file.fileName))
                {
                    ((PictureBox)chatObject.Control).Size = new Size(flp_MessageScreen.Width / 2 + 50, 200);
                    height = ((PictureBox)chatObject.Control).Height;
                }
                else
                {
                    ((Label)chatObject.Control).MaximumSize = new Size(flp_MessageScreen.Width / 2 + 50, 0);
                    height = ((Label)chatObject.Control).PreferredHeight;
                }
            }
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(chatObject.Control, "Gửi lúc : " + chatObject.CreateAt.ToString("dd/MM/yyyy HH:mm:ss"));
            FlowLayoutPanel flp = new FlowLayoutPanel
            {
                Size = new Size(flp_MessageScreen.Width - 10, height),
                Padding = new Padding(10,0,10,0)
            };
            if (chatObject.Source == Context.MyId)
            {
                flp.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                flp.FlowDirection = FlowDirection.LeftToRight;
            }
            AddControlToPanel(flp, chatObject.Control);
            AddControlToPanel(flp_MessageScreen, flp,IsPastObject);
            flp_MessageScreen.ScrollControlIntoView(flp);
        }
    }
}
