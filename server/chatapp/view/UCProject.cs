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
    public partial class UCProject : UserControl
    {
        private ProjectRepository projectRepository = new ProjectRepository();
        private DepartmentRepository departmentRepository = new DepartmentRepository();
        public UCProject()
        {
            InitializeComponent();
        }

        private void UCProject_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            List<Project> projects = new List<Project>();
            projects = projectRepository.GetAllProject();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = projects;

            cbDepartmentId.DataSource = departmentRepository.GetDepartments();
            cbDepartmentId.DisplayMember = "Name"; // Hiển thị tên phòng ban
            cbDepartmentId.ValueMember = "Id"; // Giá trị là ID của phòng ban
            cbDepartmentId.SelectedIndex = -1; // Không chọn mặc định
            //dataGridView1.Columns["Id"].Visible = false; 

            dataGridView1.Columns["Name"].HeaderText = "Tên dự án";
            dataGridView1.Columns["Description"].HeaderText = "Mô tả dự án";
            dataGridView1.Columns["DepartmentId"].HeaderText = "Mã phòng ban";
            dataGridView1.Columns["CreateAt"].HeaderText = "Ngày bắt đầu";
            dataGridView1.Columns["Finished"].HeaderText = "Đã hoàn thành";
        }

        private void LoadData(List<Project> projects)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = projects;

            cbDepartmentId.DataSource = departmentRepository.GetDepartments();
            cbDepartmentId.DisplayMember = "Name"; // Hiển thị tên phòng ban
            cbDepartmentId.ValueMember = "Id"; // Giá trị là ID của phòng ban
            cbDepartmentId.SelectedIndex = -1; // Không chọn mặc định
            //dataGridView1.Columns["Id"].Visible = false; 

            dataGridView1.Columns["Name"].HeaderText = "Tên dự án";
            dataGridView1.Columns["Description"].HeaderText = "Mô tả dự án";
            dataGridView1.Columns["DepartmentId"].HeaderText = "Mã phòng ban";
            dataGridView1.Columns["CreateAt"].HeaderText = "Ngày bắt đầu";
            dataGridView1.Columns["Finished"].HeaderText = "Đã hoàn thành";
        }


        private void xemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string finishedStatus = dataGridView1.SelectedRows[0].Cells["Finished"].Value.ToString() == "True" ? "Đã hoàn thành" : "Chưa hoàn thành";
            int countUserJoin = projectRepository.countUserJoinProject((int)dataGridView1.CurrentRow.Cells["Id"].Value);
            Department department = departmentRepository.GetDepartmentById((int)dataGridView1.SelectedRows[0].Cells["DepartmentId"].Value);
            MessageBox.Show(
                "Tên: " + dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString() + "\n" +
                "Mô tả: " + dataGridView1.SelectedRows[0].Cells["Description"].Value.ToString() + "\n" +
                "Tên phòng phụ trách: " + department.Name.ToString() + "\n" +
                "Ngày bắt đầu: " + dataGridView1.SelectedRows[0].Cells["CreateAt"].Value.ToString() + "\n" +
                "Trạng thái: " + finishedStatus + "\n" + 
                "Số người tham gia: " + countUserJoin.ToString() + "\n",
                "Thông tin dự án",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void danhSáchNhữngNgườiThamGiaDựÁnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int projectId = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
            List<UserProject> users = projectRepository.GetUsersInProject(projectId);
            FormListUserInProject form = new FormListUserInProject(users, projectId);
            form.ShowDialog();

        }

        private void đánhDấuĐãHoànThànhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int projectId = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
            if (projectRepository.MarkProjectAsFinished(projectId))
            {
                MessageBox.Show("Đánh dấu dự án đã hoàn thành thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {   
                MessageBox.Show("Đánh dấu dự án đã hoàn thành thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void đánhDấuChưaHoànThànhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int projectId = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
            if (projectRepository.MarkProjectAsUnfinished(projectId))
            {
                MessageBox.Show("Đánh dấu dự án chưa hoàn thành thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show("Đánh dấu dự án chưa hoàn thành thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Length == 0 || txtDescription.Text.Length == 0 || cbDepartmentId.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Project project = new Project
            {
                Name = txtName.Text,
                Description = txtDescription.Text,
                DepartmentId = (int)cbDepartmentId.SelectedValue,
                CreateAt = DateTime.Now,
                Finished = false // Mặc định là chưa hoàn thành
            };

            if (projectRepository.AddProject(project))
            {
                MessageBox.Show("Thêm dự án thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                txtName.Clear();
                txtDescription.Clear();
                cbDepartmentId.SelectedIndex = -1; // Reset combobox
            }
            else
            {
                MessageBox.Show("Thêm dự án thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sắpXếpTheoTênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Project> projects = projectRepository.GetAndSortByName();
            LoadData(projects);
        }

        private void sắpXếpTheoMãPhòngBanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Project> projects = projectRepository.GetAndSortByDepartmentId();
            LoadData(projects);
        }
    }
}
