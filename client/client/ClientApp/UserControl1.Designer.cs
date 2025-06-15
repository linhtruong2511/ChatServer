namespace ClientApp
{
    partial class UCcontacts
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
            this.pic_Avt = new FontAwesome.Sharp.IconPictureBox();
            this.lb_Username = new System.Windows.Forms.Label();
            this.lb_Status = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Avt)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_Avt
            // 
            this.pic_Avt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.pic_Avt.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pic_Avt.IconChar = FontAwesome.Sharp.IconChar.None;
            this.pic_Avt.IconColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pic_Avt.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.pic_Avt.IconSize = 108;
            this.pic_Avt.Location = new System.Drawing.Point(3, 3);
            this.pic_Avt.Name = "pic_Avt";
            this.pic_Avt.Size = new System.Drawing.Size(108, 109);
            this.pic_Avt.TabIndex = 0;
            this.pic_Avt.TabStop = false;
            // 
            // lb_Username
            // 
            this.lb_Username.AutoSize = true;
            this.lb_Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Username.ForeColor = System.Drawing.Color.White;
            this.lb_Username.Location = new System.Drawing.Point(117, 16);
            this.lb_Username.Name = "lb_Username";
            this.lb_Username.Size = new System.Drawing.Size(92, 32);
            this.lb_Username.TabIndex = 1;
            this.lb_Username.Text = "label1";
            // 
            // lb_Status
            // 
            this.lb_Status.AutoSize = true;
            this.lb_Status.ForeColor = System.Drawing.Color.White;
            this.lb_Status.Location = new System.Drawing.Point(117, 55);
            this.lb_Status.Name = "lb_Status";
            this.lb_Status.Size = new System.Drawing.Size(64, 25);
            this.lb_Status.TabIndex = 2;
            this.lb_Status.Text = "label2";
            // 
            // UCcontacts
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.Controls.Add(this.lb_Status);
            this.Controls.Add(this.lb_Username);
            this.Controls.Add(this.pic_Avt);
            this.Name = "UCcontacts";
            this.Size = new System.Drawing.Size(412, 112);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Avt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconPictureBox pic_Avt;
        private System.Windows.Forms.Label lb_Username;
        private System.Windows.Forms.Label lb_Status;
    }
}
