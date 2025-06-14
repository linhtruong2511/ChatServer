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
    public partial class UCUser : UserControl
    {
        private UserRepository userRepository = new UserRepository();
        private List<User> users = new List<User>();
        public UCUser()
        {
            InitializeComponent();
        }

        public UCUser(List<User> users)
        {
            InitializeComponent();
            this.users = users;
            InitData();
        }
        private void SetHeaderText()
        {
            dataGridView1.Columns["ID"].HeaderText = "ID";
            dataGridView1.Columns["Name"].HeaderText = "Họ và Tên";
            dataGridView1.Columns["Username"].HeaderText = "Tên đăng nhập";
            dataGridView1.Columns["DepartmentId"].HeaderText = "Mã Phòng ban";
            dataGridView1.Columns["PositionId"].HeaderText = "Mã Chức vụ";
        }
        private void UCUser_Load(object sender, EventArgs e)
        {
            InitData();
            SetCombobox();
            if (users.Count == 0)
            {
                users = userRepository.GetAllUser();
            }
            ReloadData(users);
        }

        private void SetCombobox()
        {
            List<Department> departments = new DepartmentRepository().GetDepartments();
            List<Position> positions = new PositionRepository().GetAllPostsition();

            // Thêm dữ liệu vào ComboBox Phòng ban
            cbDepartment.DataSource = departments;
            cbDepartment.DisplayMember = "Name"; // Hiển thị tên phòng ban
            cbDepartment.ValueMember = "Id"; // Giá trị là ID của phòng ban
            cbDepartment.SelectedIndex = -1; // Không chọn mặc định

            // Thêm dữ liệu vào ComboBox Chức vụ
            cbPosition.DataSource = positions;
            cbPosition.DisplayMember = "Name"; // Hiển thị tên chức vụ
            cbPosition.ValueMember = "Id"; // Giá trị là ID của chức vụ
            cbPosition.SelectedIndex = -1; // Không chọn mặc định
        }

        private void SetDataGridViewProperties(List<User> users)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn toàn bộ hàng khi click
            dataGridView1.MultiSelect = false; // Không cho phép chọn nhiều hàng cùng lúc
            dataGridView1.AllowUserToAddRows = false; // Không cho phép thêm hàng mới
            dataGridView1.AllowUserToDeleteRows = false; // Không cho phép xóa hàng
            dataGridView1.ReadOnly = true; // Đặt DataGridView ở chế độ chỉ đọc

            dataGridView1.DataSource = null; // Đặt DataSource về null trước khi cập nhật
            dataGridView1.DataSource = users.Select(u => new
            {
                u.ID,
                u.Name,
                u.Username,
                u.DepartmentId,
                u.PositionId
            }).ToList();
        }

        public void InitData()
        {
            SetDataGridViewProperties(this.users);
            SetHeaderText();
        }

        private void ReloadData()
        {
            users = userRepository.GetAllUser();
            SetDataGridViewProperties(users);
            SetHeaderText();
        }

        private void ReloadData(List<User> users)
        {
            SetDataGridViewProperties(users);
            SetHeaderText();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Kiểm tra các trường thông tin bắt buộc
            if (
                string.IsNullOrWhiteSpace(txtName.Text) || 
                string.IsNullOrWhiteSpace(txtUsername.Text) || 
                string.IsNullOrWhiteSpace(txtPhone.Text) || 
                string.IsNullOrWhiteSpace(txtAddress.Text) || 
                cbDepartment.SelectedIndex == -1 || 
                cbPosition.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User user = new User
            {
                Name = txtName.Text,
                Username = txtUsername.Text,
                Phone = txtPhone.Text,
                Address = txtAddress.Text,
                Password = "123456", // Mật khẩu mặc định, nên thay đổi sau
                CreateAt = DateTime.Now,
                Status = false, // Trạng thái mặc định là không hoạt động
                PositionId = (int)cbPosition.SelectedValue,
                DepartmentId = (int)cbDepartment.SelectedValue
            };

            userRepository.AddUser(user);
            ReloadData();
        }

        private void extraSearch_Click(object sender, EventArgs e)
        {
            FormExtraSearch formExtraSearch = new FormExtraSearch();
            if(formExtraSearch.ShowDialog() == DialogResult.OK)
            {
                List<User> filteredUsers = userRepository.SearchUserByFullInformation(
                    formExtraSearch.User
                );
                if (filteredUsers.Count > 0)
                {
                    ReloadData(filteredUsers);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy người dùng phù hợp với tiêu chí tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadData(); // Hiển thị lại toàn bộ người dùng nếu không có kết quả
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void xemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User user = userRepository.GetUserByUsername(dataGridView1.SelectedRows[0].Cells["Username"].Value.ToString());
            if (user == null)
            {
                MessageBox.Show("Không tìm thấy người dùng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                "Thông tin người dùng:\n" +
                "Họ và Tên: " + user.Name + "\n" +
                "Tên đăng nhập: " + user.Username + "\n" +
                "Số điện thoại: " + user.Phone + "\n" +
                "Địa chỉ: " + user.Address + "\n" +
                "Tên Phòng ban: " + new DepartmentRepository().GetDepartmentById(user.DepartmentId).Name + "\n" +
                "Tên Chức vụ: " + new PositionRepository().GetPositionById(user.PositionId).Name,
                "Thông tin người dùng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string username = dataGridView1.SelectedRows[0].Cells["Username"].Value.ToString();
            User user = userRepository.GetUserByUsername(username);
            if (user == null)
            {
                MessageBox.Show("Không tìm thấy người dùng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                userRepository.DeleteUserById(user.ID);
                ReloadData();
                MessageBox.Show("Đã xóa người dùng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toànBộNhânSựPhòngBanNàyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<User> users = userRepository.GetAllUsersByDepartmentId((int)dataGridView1.SelectedRows[0].Cells["DepartmentId"].Value);
            ReloadData(users);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void toànBộNhânSựCủaVịTríToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<User> users = userRepository.GetAllUsersByPositionId((int)dataGridView1.SelectedRows[0].Cells["PositionId"].Value);
            ReloadData(users);
        }

        private void txtSearchUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                e.SuppressKeyPress = true; // Ngăn chặn âm thanh "ding" khi nhấn Enter
                int userId;
                if (int.TryParse(txtSearchUserId.Text, out userId) && userId > 0)
                {
                    User user = userRepository.GetUserById(userId);
                    if (user != null)
                    {
                        ReloadData(new List<User> { user });
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy người dùng với ID này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập ID người dùng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
