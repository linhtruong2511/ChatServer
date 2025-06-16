namespace ClientApp
{
    partial class FormChangePassword
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
            this.btn_changePassword = new System.Windows.Forms.Button();
            this.txt_newPassword = new System.Windows.Forms.MaskedTextBox();
            this.txt_oldPassword = new System.Windows.Forms.TextBox();
            this.lbl_newPassword = new System.Windows.Forms.Label();
            this.lbl_oldPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_changePassword
            // 
            this.btn_changePassword.Location = new System.Drawing.Point(200, 150);
            this.btn_changePassword.Name = "btn_changePassword";
            this.btn_changePassword.Size = new System.Drawing.Size(113, 23);
            this.btn_changePassword.TabIndex = 9;
            this.btn_changePassword.Text = "Đổi mật khẩu";
            this.btn_changePassword.UseVisualStyleBackColor = true;
            // 
            // txt_newPassword
            // 
            this.txt_newPassword.Location = new System.Drawing.Point(200, 112);
            this.txt_newPassword.Name = "txt_newPassword";
            this.txt_newPassword.Size = new System.Drawing.Size(164, 22);
            this.txt_newPassword.TabIndex = 8;
            // 
            // txt_oldPassword
            // 
            this.txt_oldPassword.Location = new System.Drawing.Point(200, 71);
            this.txt_oldPassword.Name = "txt_oldPassword";
            this.txt_oldPassword.Size = new System.Drawing.Size(164, 22);
            this.txt_oldPassword.TabIndex = 7;
            // 
            // lbl_newPassword
            // 
            this.lbl_newPassword.AutoSize = true;
            this.lbl_newPassword.Location = new System.Drawing.Point(93, 112);
            this.lbl_newPassword.Name = "lbl_newPassword";
            this.lbl_newPassword.Size = new System.Drawing.Size(100, 16);
            this.lbl_newPassword.TabIndex = 6;
            this.lbl_newPassword.Text = "Mật khẩu mới : ";
            // 
            // lbl_oldPassword
            // 
            this.lbl_oldPassword.AutoSize = true;
            this.lbl_oldPassword.Location = new System.Drawing.Point(93, 71);
            this.lbl_oldPassword.Name = "lbl_oldPassword";
            this.lbl_oldPassword.Size = new System.Drawing.Size(91, 16);
            this.lbl_oldPassword.TabIndex = 5;
            this.lbl_oldPassword.Text = "Mật khẩu cũ : ";
            // 
            // FormChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 245);
            this.Controls.Add(this.btn_changePassword);
            this.Controls.Add(this.txt_newPassword);
            this.Controls.Add(this.txt_oldPassword);
            this.Controls.Add(this.lbl_newPassword);
            this.Controls.Add(this.lbl_oldPassword);
            this.Name = "FormChangePassword";
            this.Text = "FormChangePassword";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btn_changePassword;
        public System.Windows.Forms.MaskedTextBox txt_newPassword;
        public System.Windows.Forms.TextBox txt_oldPassword;
        public System.Windows.Forms.Label lbl_newPassword;
        public System.Windows.Forms.Label lbl_oldPassword;
    }
}