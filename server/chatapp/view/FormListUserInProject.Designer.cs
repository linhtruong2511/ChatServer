namespace chatapp.view
{
    partial class FormListUserInProject
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.thêmNgườiThamGiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xóaNgườiThamGiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(800, 450);
            this.dataGridView1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thêmNgườiThamGiaToolStripMenuItem,
            this.xóaNgườiThamGiaToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(189, 70);
            // 
            // thêmNgườiThamGiaToolStripMenuItem
            // 
            this.thêmNgườiThamGiaToolStripMenuItem.Name = "thêmNgườiThamGiaToolStripMenuItem";
            this.thêmNgườiThamGiaToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.thêmNgườiThamGiaToolStripMenuItem.Text = "Thêm người tham gia";
            this.thêmNgườiThamGiaToolStripMenuItem.Click += new System.EventHandler(this.thêmNgườiThamGiaToolStripMenuItem_Click);
            // 
            // xóaNgườiThamGiaToolStripMenuItem
            // 
            this.xóaNgườiThamGiaToolStripMenuItem.Name = "xóaNgườiThamGiaToolStripMenuItem";
            this.xóaNgườiThamGiaToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.xóaNgườiThamGiaToolStripMenuItem.Text = "Xóa người tham gia";
            this.xóaNgườiThamGiaToolStripMenuItem.Click += new System.EventHandler(this.xóaNgườiThamGiaToolStripMenuItem_Click);
            // 
            // FormListUserInProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormListUserInProject";
            this.Text = "FormListUserInProject";
            this.Load += new System.EventHandler(this.FormListUserInProject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem thêmNgườiThamGiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xóaNgườiThamGiaToolStripMenuItem;
    }
}