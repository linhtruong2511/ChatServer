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
    public partial class FormListUserInProject : Form
    {
        private List<UserProject> users = new List<UserProject>();
        private UserRepository userRepository = new UserRepository();
        private ProjectRepository projectRepository = new ProjectRepository();
        private int projectID;
        public FormListUserInProject(List<UserProject> users, int projectID)
        {
            InitializeComponent();
            this.users = users;
            this.projectID = projectID;
        }

        private void FormListUserInProject_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = users;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn toàn bộ hàng khi click
            dataGridView1.MultiSelect = false; // Không cho phép chọn nhiều hàng cùng lúc

            dataGridView1.Columns["Name"].HeaderText = "Họ và Tên"; // Đổi tên cột
            dataGridView1.Columns["Role"].HeaderText = "Vai trò"; // Đổi tên cột
            dataGridView1.Columns["Phone"].HeaderText = "Số điện thoại"; // Đổi tên cột
            dataGridView1.Columns["Address"].HeaderText = "Địa chỉ"; // Đổi tên cột
        }

        private void thêmNgườiThamGiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddUserToProject formAddUserToProject = new FormAddUserToProject();
            if (formAddUserToProject.ShowDialog() == DialogResult.OK)
            {
                // Giả sử bạn đã thêm người dùng mới vào danh sách users
                // Cập nhật lại DataGridView
                int userId = -1;
                try
                {
                    userId = int.Parse(formAddUserToProject.txtUserID.Text); // Lấy ID người dùng từ form
                } catch (FormatException)
                {
                    MessageBox.Show("ID người dùng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string role = formAddUserToProject.txtRole.Text; // Lấy vai trò từ form

                User user = userRepository.GetUserById(userId);

                users.Add(new UserProject()
                {
                    Id = userId,
                    Name = user.Name,
                    Role = role,
                    Phone = user.Phone,
                    Address = user.Address,
                }); // Thêm người dùng mới vào danh sách

                projectRepository.AddUserToProject(user,this.projectID,role );

                MessageBox.Show("Thêm người tham gia thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


                dataGridView1.DataSource = null;
                dataGridView1.DataSource = users;
            }
        }

        private void xóaNgườiThamGiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userId = dataGridView1.SelectedRows[0].Cells["ID"].Value != null ? Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value) : -1;
            if (userId == -1)
            {
                MessageBox.Show("Vui lòng chọn người dùng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa người dùng này khỏi dự án không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes)
            {
                users.Remove(users.FirstOrDefault(u => u.Id == userId)); // Xóa người dùng khỏi danh sách
                projectRepository.RemoveUserInProject(userId);
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = users;

            MessageBox.Show(
                "Xóa người tham gia thành công.", 
                "Thông báo", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information
            );
        }
    }
}
