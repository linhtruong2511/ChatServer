namespace ClientApp
{
    partial class MainForm
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
            this.flp_UsersScreen = new System.Windows.Forms.FlowLayoutPanel();
            this.flp_MessageScreen = new System.Windows.Forms.FlowLayoutPanel();
            this.txt_Message = new System.Windows.Forms.TextBox();
            this.btn_SendMessage = new System.Windows.Forms.Button();
            this.btn_FindUser = new System.Windows.Forms.Button();
            this.txt_FindUser = new System.Windows.Forms.TextBox();
            this.btn_SendFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flp_UsersScreen
            // 
            this.flp_UsersScreen.AutoScroll = true;
            this.flp_UsersScreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flp_UsersScreen.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp_UsersScreen.Location = new System.Drawing.Point(18, 55);
            this.flp_UsersScreen.Name = "flp_UsersScreen";
            this.flp_UsersScreen.Size = new System.Drawing.Size(219, 383);
            this.flp_UsersScreen.TabIndex = 0;
            // 
            // flp_MessageScreen
            // 
            this.flp_MessageScreen.AutoScroll = true;
            this.flp_MessageScreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flp_MessageScreen.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp_MessageScreen.Location = new System.Drawing.Point(243, 19);
            this.flp_MessageScreen.Name = "flp_MessageScreen";
            this.flp_MessageScreen.Size = new System.Drawing.Size(328, 357);
            this.flp_MessageScreen.TabIndex = 1;
            this.flp_MessageScreen.WrapContents = false;
            // 
            // txt_Message
            // 
            this.txt_Message.Location = new System.Drawing.Point(356, 382);
            this.txt_Message.MaxLength = 100;
            this.txt_Message.Multiline = true;
            this.txt_Message.Name = "txt_Message";
            this.txt_Message.Size = new System.Drawing.Size(215, 56);
            this.txt_Message.TabIndex = 2;
            // 
            // btn_SendMessage
            // 
            this.btn_SendMessage.Location = new System.Drawing.Point(243, 382);
            this.btn_SendMessage.Name = "btn_SendMessage";
            this.btn_SendMessage.Size = new System.Drawing.Size(52, 56);
            this.btn_SendMessage.TabIndex = 3;
            this.btn_SendMessage.Text = "Gửi tin nhắn";
            this.btn_SendMessage.UseVisualStyleBackColor = true;
            this.btn_SendMessage.Click += new System.EventHandler(this.btn_SendMessage_Click);
            // 
            // btn_FindUser
            // 
            this.btn_FindUser.Location = new System.Drawing.Point(171, 22);
            this.btn_FindUser.Name = "btn_FindUser";
            this.btn_FindUser.Size = new System.Drawing.Size(66, 23);
            this.btn_FindUser.TabIndex = 4;
            this.btn_FindUser.Text = "tìm kiếm";
            this.btn_FindUser.UseVisualStyleBackColor = true;
            // 
            // txt_FindUser
            // 
            this.txt_FindUser.Location = new System.Drawing.Point(18, 23);
            this.txt_FindUser.Name = "txt_FindUser";
            this.txt_FindUser.Size = new System.Drawing.Size(147, 22);
            this.txt_FindUser.TabIndex = 5;
            // 
            // btn_SendFile
            // 
            this.btn_SendFile.Location = new System.Drawing.Point(301, 382);
            this.btn_SendFile.Name = "btn_SendFile";
            this.btn_SendFile.Size = new System.Drawing.Size(49, 56);
            this.btn_SendFile.TabIndex = 6;
            this.btn_SendFile.Text = "Gửi file";
            this.btn_SendFile.UseVisualStyleBackColor = true;
            this.btn_SendFile.Click += new System.EventHandler(this.btn_SendFile_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 450);
            this.Controls.Add(this.btn_SendFile);
            this.Controls.Add(this.txt_FindUser);
            this.Controls.Add(this.btn_FindUser);
            this.Controls.Add(this.btn_SendMessage);
            this.Controls.Add(this.txt_Message);
            this.Controls.Add(this.flp_MessageScreen);
            this.Controls.Add(this.flp_UsersScreen);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flp_UsersScreen;
        private System.Windows.Forms.FlowLayoutPanel flp_MessageScreen;
        private System.Windows.Forms.TextBox txt_Message;
        private System.Windows.Forms.Button btn_SendMessage;
        private System.Windows.Forms.Button btn_FindUser;
        private System.Windows.Forms.TextBox txt_FindUser;
        private System.Windows.Forms.Button btn_SendFile;
    }
}

