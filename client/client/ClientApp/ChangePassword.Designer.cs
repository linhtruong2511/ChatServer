namespace ClientApp
{
    partial class ChangePassword
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_oldPassword = new System.Windows.Forms.Label();
            this.lbl_newPassword = new System.Windows.Forms.Label();
            this.txt_oldPassword = new System.Windows.Forms.TextBox();
            this.txt_newPassword = new System.Windows.Forms.MaskedTextBox();
            this.btn_changePassword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_oldPassword
            // 
            this.lbl_oldPassword.AutoSize = true;
            this.lbl_oldPassword.Location = new System.Drawing.Point(3, 32);
            this.lbl_oldPassword.Name = "lbl_oldPassword";
            this.lbl_oldPassword.Size = new System.Drawing.Size(91, 16);
            this.lbl_oldPassword.TabIndex = 0;
            this.lbl_oldPassword.Text = "Mật khẩu cũ : ";
            // 
            // lbl_newPassword
            // 
            this.lbl_newPassword.AutoSize = true;
            this.lbl_newPassword.Location = new System.Drawing.Point(3, 73);
            this.lbl_newPassword.Name = "lbl_newPassword";
            this.lbl_newPassword.Size = new System.Drawing.Size(100, 16);
            this.lbl_newPassword.TabIndex = 1;
            this.lbl_newPassword.Text = "Mật khẩu mới : ";
            this.lbl_newPassword.Click += new System.EventHandler(this.lbl_newPassword_Click);
            // 
            // txt_oldPassword
            // 
            this.txt_oldPassword.Location = new System.Drawing.Point(110, 32);
            this.txt_oldPassword.Name = "txt_oldPassword";
            this.txt_oldPassword.Size = new System.Drawing.Size(164, 22);
            this.txt_oldPassword.TabIndex = 2;
            // 
            // txt_newPassword
            // 
            this.txt_newPassword.Location = new System.Drawing.Point(110, 73);
            this.txt_newPassword.Name = "txt_newPassword";
            this.txt_newPassword.Size = new System.Drawing.Size(164, 22);
            this.txt_newPassword.TabIndex = 3;
            // 
            // btn_changePassword
            // 
            this.btn_changePassword.Location = new System.Drawing.Point(110, 111);
            this.btn_changePassword.Name = "btn_changePassword";
            this.btn_changePassword.Size = new System.Drawing.Size(113, 23);
            this.btn_changePassword.TabIndex = 4;
            this.btn_changePassword.Text = "Đổi mật khẩu";
            this.btn_changePassword.UseVisualStyleBackColor = true;
            // 
            // ChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_changePassword);
            this.Controls.Add(this.txt_newPassword);
            this.Controls.Add(this.txt_oldPassword);
            this.Controls.Add(this.lbl_newPassword);
            this.Controls.Add(this.lbl_oldPassword);
            this.Name = "ChangePassword";
            this.Size = new System.Drawing.Size(345, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_oldPassword;
        public System.Windows.Forms.Label lbl_newPassword;
        public System.Windows.Forms.TextBox txt_oldPassword;
        public System.Windows.Forms.MaskedTextBox txt_newPassword;
        public System.Windows.Forms.Button btn_changePassword;
    }
}
