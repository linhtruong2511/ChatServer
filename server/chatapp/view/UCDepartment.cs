using chatapp.model;
using chatapp.repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chatapp.view
{
    public partial class UCDepartment : UserControl
    {
        private DepartmentRepository departmentRepository = new DepartmentRepository();
        private UserRepository userRepository = new UserRepository();
        Panel parentPanel = null;
        public UCDepartment(Panel parentPanel)
        {
            InitializeComponent();
            this.parentPanel = parentPanel;
        }

        private void UCDepartment_Load(object sender, EventArgs e)
        {
            LoadData(); 
        }

        private void xemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dgvDepartment.SelectedRows[0];
            if (selectedRow != null)
            {
                int departmentId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                string departmentName = selectedRow.Cells["Name"].Value.ToString();
                MessageBox.Show($"Mã phòng ban: {departmentId}\nTên phòng ban: {departmentName}", "Department Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a department to view details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadData()
        {
            List<Department> departments = departmentRepository.GetDepartments();
            if (departments.Count > 0)
            {
                this.dgvDepartment.DataSource = departments;
                this.dgvDepartment.Columns["Id"].Visible = false; 
                this.dgvDepartment.Columns["Name"].HeaderText = "Tên phòng ban"; // Set header text for clarity
            }
            else
            {
                MessageBox.Show("No departments found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            DialogResult result = MessageBox.Show(
                "Bạn có muốn xóa óa phòng ban " + 
                dgvDepartment.SelectedRows[0].Cells["Name"].Value + 
                " không? Khi xóa không thể hoàn tác lại và toàn bộ dữ liệu liên quan tới phòng ban này sẽ bị xóa",

                "Success",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question
                );

            if (result == DialogResult.OK)
            {
                departmentRepository.DeleteDepartment(
                    Convert.ToInt32(dgvDepartment.SelectedRows[0].Cells["Id"].Value)
                );
                LoadData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            departmentRepository.AddDepartment(new Department { Name = txtDepartmentName.Text });
            LoadData();
            MessageBox.Show(
                "Thêm phòng ban " + txtDepartmentName.Text + " thành công",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
        }



        private void sửaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvDepartment.SelectedRows.Count > 0)
            {
                Department selectedDepartment = new Department
                {
                    Id = Convert.ToInt32(dgvDepartment.SelectedRows[0].Cells["Id"].Value),
                    Name = dgvDepartment.SelectedRows[0].Cells["Name"].Value.ToString()
                };
                departmentRepository.UpdateDepartment(selectedDepartment);
                LoadData();
                MessageBox.Show(
                    "Cập nhật thành công",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }
            else
            {
                MessageBox.Show("Please select a department to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void nhânViênCủaPhòngNàyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<User> users = userRepository.GetAllUsersByDepartmentId(
                Convert.ToInt32(dgvDepartment.SelectedRows[0].Cells["Id"].Value)
            );
            parentPanel.Controls.Clear();
            UCUser uCUser = new UCUser(users);
            parentPanel.Controls.Add(uCUser);
        }
    }
}
