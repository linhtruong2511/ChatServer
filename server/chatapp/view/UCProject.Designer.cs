namespace chatapp.view
{
    partial class UCProject
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
            this.xemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xóaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.danhSáchNhữngNgườiThamGiaDựÁnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đánhDấuĐãHoànThànhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đánhDấuChưaHoànThànhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sắpXếpTheoTênToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sắpXếpTheoMãPhòngBanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cbDepartmentId = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(5, 255);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(811, 254);
            this.dataGridView1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xemToolStripMenuItem,
            this.xóaToolStripMenuItem,
            this.danhSáchNhữngNgườiThamGiaDựÁnToolStripMenuItem,
            this.đánhDấuĐãHoànThànhToolStripMenuItem,
            this.đánhDấuChưaHoànThànhToolStripMenuItem,
            this.sắpXếpTheoTênToolStripMenuItem,
            this.sắpXếpTheoMãPhòngBanToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(229, 180);
            // 
            // xemToolStripMenuItem
            // 
            this.xemToolStripMenuItem.Name = "xemToolStripMenuItem";
            this.xemToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.xemToolStripMenuItem.Text = "Xem thông tin";
            this.xemToolStripMenuItem.Click += new System.EventHandler(this.xemToolStripMenuItem_Click);
            // 
            // xóaToolStripMenuItem
            // 
            this.xóaToolStripMenuItem.Name = "xóaToolStripMenuItem";
            this.xóaToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.xóaToolStripMenuItem.Text = "Xóa  ";
            // 
            // danhSáchNhữngNgườiThamGiaDựÁnToolStripMenuItem
            // 
            this.danhSáchNhữngNgườiThamGiaDựÁnToolStripMenuItem.Name = "danhSáchNhữngNgườiThamGiaDựÁnToolStripMenuItem";
            this.danhSáchNhữngNgườiThamGiaDựÁnToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.danhSáchNhữngNgườiThamGiaDựÁnToolStripMenuItem.Text = "Những người tham gia dự án";
            this.danhSáchNhữngNgườiThamGiaDựÁnToolStripMenuItem.Click += new System.EventHandler(this.danhSáchNhữngNgườiThamGiaDựÁnToolStripMenuItem_Click);
            // 
            // đánhDấuĐãHoànThànhToolStripMenuItem
            // 
            this.đánhDấuĐãHoànThànhToolStripMenuItem.Name = "đánhDấuĐãHoànThànhToolStripMenuItem";
            this.đánhDấuĐãHoànThànhToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.đánhDấuĐãHoànThànhToolStripMenuItem.Text = "Đánh dấu đã hoàn thành";
            this.đánhDấuĐãHoànThànhToolStripMenuItem.Click += new System.EventHandler(this.đánhDấuĐãHoànThànhToolStripMenuItem_Click);
            // 
            // đánhDấuChưaHoànThànhToolStripMenuItem
            // 
            this.đánhDấuChưaHoànThànhToolStripMenuItem.Name = "đánhDấuChưaHoànThànhToolStripMenuItem";
            this.đánhDấuChưaHoànThànhToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.đánhDấuChưaHoànThànhToolStripMenuItem.Text = "Đánh dấu chưa hoàn thành";
            this.đánhDấuChưaHoànThànhToolStripMenuItem.Click += new System.EventHandler(this.đánhDấuChưaHoànThànhToolStripMenuItem_Click);
            // 
            // sắpXếpTheoTênToolStripMenuItem
            // 
            this.sắpXếpTheoTênToolStripMenuItem.Name = "sắpXếpTheoTênToolStripMenuItem";
            this.sắpXếpTheoTênToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.sắpXếpTheoTênToolStripMenuItem.Text = "Sắp xếp theo tên";
            this.sắpXếpTheoTênToolStripMenuItem.Click += new System.EventHandler(this.sắpXếpTheoTênToolStripMenuItem_Click);
            // 
            // sắpXếpTheoMãPhòngBanToolStripMenuItem
            // 
            this.sắpXếpTheoMãPhòngBanToolStripMenuItem.Name = "sắpXếpTheoMãPhòngBanToolStripMenuItem";
            this.sắpXếpTheoMãPhòngBanToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.sắpXếpTheoMãPhòngBanToolStripMenuItem.Text = "Sắp xếp theo mã phòng ban";
            this.sắpXếpTheoMãPhòngBanToolStripMenuItem.Click += new System.EventHandler(this.sắpXếpTheoMãPhòngBanToolStripMenuItem_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(150, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(652, 20);
            this.txtName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tên dự án";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mã phòng ban phụ trách";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Mô tả dự án";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(150, 58);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(652, 93);
            this.txtDescription.TabIndex = 6;
            // 
            // cbDepartmentId
            // 
            this.cbDepartmentId.FormattingEnabled = true;
            this.cbDepartmentId.Location = new System.Drawing.Point(150, 169);
            this.cbDepartmentId.Name = "cbDepartmentId";
            this.cbDepartmentId.Size = new System.Drawing.Size(652, 21);
            this.cbDepartmentId.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(681, 209);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Thêm ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UCProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbDepartmentId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.dataGridView1);
            this.Name = "UCProject";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(821, 514);
            this.Load += new System.EventHandler(this.UCProject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem xemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xóaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem danhSáchNhữngNgườiThamGiaDựÁnToolStripMenuItem;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ComboBox cbDepartmentId;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem đánhDấuĐãHoànThànhToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đánhDấuChưaHoànThànhToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sắpXếpTheoTênToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sắpXếpTheoMãPhòngBanToolStripMenuItem;
    }
}
