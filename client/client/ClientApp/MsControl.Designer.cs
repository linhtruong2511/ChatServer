namespace ClientApp
{
    partial class MsControl
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
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.lb_time = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lb_message = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2CustomGradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.AutoSize = true;
            this.guna2CustomGradientPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(47)))));
            this.guna2CustomGradientPanel1.BorderRadius = 25;
            this.guna2CustomGradientPanel1.Controls.Add(this.lb_time);
            this.guna2CustomGradientPanel1.Controls.Add(this.lb_message);
            this.guna2CustomGradientPanel1.Cursor = System.Windows.Forms.Cursors.No;
            this.guna2CustomGradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2CustomGradientPanel1.FillColor = System.Drawing.Color.DarkMagenta;
            this.guna2CustomGradientPanel1.FillColor2 = System.Drawing.Color.MediumPurple;
            this.guna2CustomGradientPanel1.FillColor3 = System.Drawing.Color.LavenderBlush;
            this.guna2CustomGradientPanel1.FillColor4 = System.Drawing.Color.Red;
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(194, 73);
            this.guna2CustomGradientPanel1.TabIndex = 0;
            // 
            // lb_time
            // 
            this.lb_time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_time.BackColor = System.Drawing.Color.Transparent;
            this.lb_time.ForeColor = System.Drawing.Color.White;
            this.lb_time.Location = new System.Drawing.Point(135, 44);
            this.lb_time.Name = "lb_time";
            this.lb_time.Size = new System.Drawing.Size(48, 26);
            this.lb_time.TabIndex = 3;
            this.lb_time.Text = "10:00";
            this.lb_time.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_message
            // 
            this.lb_message.BackColor = System.Drawing.Color.Transparent;
            this.lb_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_message.Location = new System.Drawing.Point(20, 14);
            this.lb_message.MaximumSize = this.MaximumSize;
            this.lb_message.Name = "lb_message";
            this.lb_message.Size = new System.Drawing.Size(171, 27);
            this.lb_message.TabIndex = 2;
            this.lb_message.Text = "guna2HtmlLabel1";
            // 
            // MsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Name = "MsControl";
            this.Size = new System.Drawing.Size(194, 73);
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.guna2CustomGradientPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lb_time;
        private Guna.UI2.WinForms.Guna2HtmlLabel lb_message;
    }
}
