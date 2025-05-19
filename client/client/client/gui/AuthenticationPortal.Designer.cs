namespace client.gui
{
    partial class AuthenticationPortal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_account = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.btn_signup = new System.Windows.Forms.Button();
            this.lbl_accountname = new System.Windows.Forms.Label();
            this.lbl_password = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_account
            // 
            this.txt_account.Location = new System.Drawing.Point(286, 200);
            this.txt_account.Name = "txt_account";
            this.txt_account.Size = new System.Drawing.Size(138, 22);
            this.txt_account.TabIndex = 0;
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(286, 238);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(138, 22);
            this.txt_password.TabIndex = 1;
            this.txt_password.UseSystemPasswordChar = true;
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(238, 266);
            this.btn_login.Name = "btn_login";
            this.btn_login.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_login.Size = new System.Drawing.Size(89, 28);
            this.btn_login.TabIndex = 2;
            this.btn_login.Text = "Đăng nhập";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // btn_signup
            // 
            this.btn_signup.Location = new System.Drawing.Point(348, 266);
            this.btn_signup.Name = "btn_signup";
            this.btn_signup.Size = new System.Drawing.Size(94, 28);
            this.btn_signup.TabIndex = 4;
            this.btn_signup.Text = "Đăng ký";
            this.btn_signup.UseVisualStyleBackColor = true;
            this.btn_signup.Click += new System.EventHandler(this.btn_signup_Click);
            // 
            // lbl_accountname
            // 
            this.lbl_accountname.AutoSize = true;
            this.lbl_accountname.Location = new System.Drawing.Point(217, 203);
            this.lbl_accountname.Name = "lbl_accountname";
            this.lbl_accountname.Size = new System.Drawing.Size(63, 16);
            this.lbl_accountname.TabIndex = 5;
            this.lbl_accountname.Text = "Tài khoản";
            // 
            // lbl_password
            // 
            this.lbl_password.AutoSize = true;
            this.lbl_password.Location = new System.Drawing.Point(217, 244);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(61, 16);
            this.lbl_password.TabIndex = 6;
            this.lbl_password.Text = "Mật khẩu";
            // 
            // AuthenticationPortal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 450);
            this.Controls.Add(this.lbl_password);
            this.Controls.Add(this.lbl_accountname);
            this.Controls.Add(this.btn_signup);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_account);
            this.Name = "AuthenticationPortal";
            this.Text = "AuthenticationPortal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_account;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Button btn_signup;
        private System.Windows.Forms.Label lbl_accountname;
        private System.Windows.Forms.Label lbl_password;
    }
}