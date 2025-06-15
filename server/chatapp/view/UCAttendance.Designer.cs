namespace chatapp.view
{
    partial class UCAttendance
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xóaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblAttendanceToday = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.chấmCôngTheoUserIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.xuấtFileExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button3 = new System.Windows.Forms.Button();
            this.sắpXếpTheoNgàyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sắpXếpTheoMãNhânViênToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hiểnThịThêmDữLiệuCũHơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hiểnThịDữLiệuMớiHơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(5, 218);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(811, 291);
            this.dataGridView1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xóaToolStripMenuItem,
            this.chấmCôngTheoUserIdToolStripMenuItem,
            this.xuấtFileExcelToolStripMenuItem,
            this.sắpXếpTheoNgàyToolStripMenuItem,
            this.sắpXếpTheoMãNhânViênToolStripMenuItem,
            this.hiểnThịThêmDữLiệuCũHơnToolStripMenuItem,
            this.hiểnThịDữLiệuMớiHơnToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(228, 158);
            // 
            // xóaToolStripMenuItem
            // 
            this.xóaToolStripMenuItem.Name = "xóaToolStripMenuItem";
            this.xóaToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.xóaToolStripMenuItem.Text = "Xóa";
            this.xóaToolStripMenuItem.Click += new System.EventHandler(this.xóaToolStripMenuItem_Click);
            // 
            // lblAttendanceToday
            // 
            this.lblAttendanceToday.Location = new System.Drawing.Point(382, 22);
            this.lblAttendanceToday.Name = "lblAttendanceToday";
            this.lblAttendanceToday.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblAttendanceToday.Size = new System.Drawing.Size(412, 23);
            this.lblAttendanceToday.TabIndex = 2;
            this.lblAttendanceToday.Text = "label Attendace";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Chấm công thủ công:";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(150, 48);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(642, 20);
            this.txtUserID.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mã nhân viên";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Giờ tới";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Giờ về";
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpCheckIn.Location = new System.Drawing.Point(150, 85);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(642, 20);
            this.dtpCheckIn.TabIndex = 12;
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpCheckOut.Location = new System.Drawing.Point(150, 121);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(642, 20);
            this.dtpCheckOut.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(686, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Tạo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chấmCôngTheoUserIdToolStripMenuItem
            // 
            this.chấmCôngTheoUserIdToolStripMenuItem.Name = "chấmCôngTheoUserIdToolStripMenuItem";
            this.chấmCôngTheoUserIdToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.chấmCôngTheoUserIdToolStripMenuItem.Text = "Hiện chấm công theo user Id";
            this.chấmCôngTheoUserIdToolStripMenuItem.Click += new System.EventHandler(this.chấmCôngTheoUserIdToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(552, 162);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "Tải lại dữ liệu";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // xuấtFileExcelToolStripMenuItem
            // 
            this.xuấtFileExcelToolStripMenuItem.Name = "xuấtFileExcelToolStripMenuItem";
            this.xuấtFileExcelToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.xuấtFileExcelToolStripMenuItem.Text = "Xuất file excel";
            this.xuấtFileExcelToolStripMenuItem.Click += new System.EventHandler(this.xuấtFileExcelToolStripMenuItem_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(27, 162);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "Xuất file excel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // sắpXếpTheoNgàyToolStripMenuItem
            // 
            this.sắpXếpTheoNgàyToolStripMenuItem.Name = "sắpXếpTheoNgàyToolStripMenuItem";
            this.sắpXếpTheoNgàyToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.sắpXếpTheoNgàyToolStripMenuItem.Text = "Sắp xếp theo ngày";
            this.sắpXếpTheoNgàyToolStripMenuItem.Click += new System.EventHandler(this.sắpXếpTheoNgàyToolStripMenuItem_Click);
            // 
            // sắpXếpTheoMãNhânViênToolStripMenuItem
            // 
            this.sắpXếpTheoMãNhânViênToolStripMenuItem.Name = "sắpXếpTheoMãNhânViênToolStripMenuItem";
            this.sắpXếpTheoMãNhânViênToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.sắpXếpTheoMãNhânViênToolStripMenuItem.Text = "Sắp xếp theo mã nhân viên";
            this.sắpXếpTheoMãNhânViênToolStripMenuItem.Click += new System.EventHandler(this.sắpXếpTheoMãNhânViênToolStripMenuItem_Click);
            // 
            // hiểnThịThêmDữLiệuCũHơnToolStripMenuItem
            // 
            this.hiểnThịThêmDữLiệuCũHơnToolStripMenuItem.Name = "hiểnThịThêmDữLiệuCũHơnToolStripMenuItem";
            this.hiểnThịThêmDữLiệuCũHơnToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.hiểnThịThêmDữLiệuCũHơnToolStripMenuItem.Text = "Hiển thị dữ liệu cũ hơn";
            this.hiểnThịThêmDữLiệuCũHơnToolStripMenuItem.Click += new System.EventHandler(this.hiểnThịThêmDữLiệuCũHơnToolStripMenuItem_Click);
            // 
            // hiểnThịDữLiệuMớiHơnToolStripMenuItem
            // 
            this.hiểnThịDữLiệuMớiHơnToolStripMenuItem.Name = "hiểnThịDữLiệuMớiHơnToolStripMenuItem";
            this.hiểnThịDữLiệuMớiHơnToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.hiểnThịDữLiệuMớiHơnToolStripMenuItem.Text = "Hiển thị dữ liệu mới hơn";
            this.hiểnThịDữLiệuMớiHơnToolStripMenuItem.Click += new System.EventHandler(this.hiểnThịDữLiệuMớiHơnToolStripMenuItem_Click);
            // 
            // UCAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtpCheckOut);
            this.Controls.Add(this.dtpCheckIn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAttendanceToday);
            this.Controls.Add(this.dataGridView1);
            this.Name = "UCAttendance";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(821, 514);
            this.Load += new System.EventHandler(this.UCAttendance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblAttendanceToday;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpCheckIn;
        private System.Windows.Forms.DateTimePicker dtpCheckOut;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem xóaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chấmCôngTheoUserIdToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem xuấtFileExcelToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripMenuItem sắpXếpTheoNgàyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sắpXếpTheoMãNhânViênToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hiểnThịThêmDữLiệuCũHơnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hiểnThịDữLiệuMớiHơnToolStripMenuItem;
    }
}
