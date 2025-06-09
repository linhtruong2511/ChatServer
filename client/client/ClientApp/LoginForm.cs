using ClientApp.common.Class;
using ClientApp.common.Enum;
using ClientApp.utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class LoginForm : Form
    {
        public MainForm MainForm { get; set; }
        public Context Context { get; set; }
        public LoginForm(Context Context, MainForm MainForm)
        {
            this.Context = Context;
            InitializeComponent();
            this.MainForm = MainForm;
        }

        private void login_Click(object sender, EventArgs e)
        {
            LoginRequest loginRequest = new LoginRequest(username.Text, password.Text);
            string loginRequestJson = JsonConvert.SerializeObject(loginRequest);
            Packet loginPacket = new Packet(PacketTypeEnum.LOGIN, Encoding.UTF8.GetBytes(loginRequestJson), 0, 0);
            NetworkUtils.Write(Context.Writer, loginPacket);
            Task loginHandle = Task.Run(()=>HandleLoginResponse());
        }
        public void HandleLoginResponse()
        {
            Packet loginResponsePacket = NetworkUtils.Read(Context.Reader);
            switch (loginResponsePacket.Type)
            {
                case PacketTypeEnum.NOTIFICATION:
                    MessageBox.Show(Encoding.UTF8.GetString(loginResponsePacket.Data), "Notification");
                    break;
                case PacketTypeEnum.SUCCESSLOGIN:
                    UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(Encoding.UTF8.GetString(loginResponsePacket.Data));
                    Context.MyId = userInfo.Id;
                    Context.Name = userInfo.Name;
                    this.Hide();
                    MainForm.Show();
                    Packet userInfoPacket = NetworkUtils.Read(Context.Reader);
                    Context.Users = JsonConvert.DeserializeObject<List<UserInfo>>(Encoding.UTF8.GetString(userInfoPacket.Data));
                    MainForm.ShowUsers();
                    MainForm.IsLoginSuccess = true;
                    break;
            }
        }
        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
