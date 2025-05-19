using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using chatapp.dto.request;
using client.server;
namespace client.gui
{
    public partial class AuthenticationPortal : Form
    {
        public App mainapp { get; set; }
        public Gui maingui { get; set; }
        public AuthenticationPortal(Gui maingui)
        {
            InitializeComponent();
            mainapp = new App();
            this.maingui = maingui;
            this.maingui.mainapp = this.mainapp;
        }
        public UserRequest GetInformation()
        {
            return new UserRequest(txt_account.Text, txt_password.Text);
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (UserService.Login(mainapp.reader, mainapp.writer, GetInformation()) == "success login")
            {
                this.Hide();
                this.maingui.Show();
                this.maingui.Run();
            }
            else
            {
                MessageBox.Show("Login fail", "Notification");
            }
        }
        private void btn_signup_Click(object sender, EventArgs e)
        {
            if (UserService.SignUp(mainapp.reader, mainapp.writer, GetInformation()) == "success sign up")
            {
                this.Hide();
                this.maingui.Show();
                this.maingui.Run();
            }
            else
            {
                MessageBox.Show("Sign up fail", "Notification");
            }
        }
    }
}
