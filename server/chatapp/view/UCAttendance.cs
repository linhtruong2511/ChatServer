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
    public partial class UCAttendance : UserControl
    {
        int page = 0;
        private AttendanceRepository attendanceRepository = new AttendanceRepository();
        public UCAttendance()
        {
            InitializeComponent();
        }

        private void UCAttendance_Load(object sender, EventArgs e)
        {
            LoadData();
            this.lblAttendanceToday.Text = "Số lượng nhân viên chấm công ngày (" + DateTime.Now.ToString("dd/MM/yyyy") + "): " + attendanceRepository.CountAttendanceToday();
        }

        private void LoadData()
        {
            List<Attendance> attendanceList = attendanceRepository.GetAllAttendance();
            if (attendanceList != null && attendanceList.Count > 0)
            {
                dataGridView1.DataSource = attendanceList;

                dataGridView1.Columns["Id"].Visible = false; // Hide Id column
                dataGridView1.Columns["UserId"].HeaderText = "Mã nhân viên"; // Rename UserId column
                dataGridView1.Columns["Date"].HeaderText = "Ngày"; // Rename
                dataGridView1.Columns["CheckIn"].HeaderText = "Giờ vào"; // Rename CheckIn column
                dataGridView1.Columns["CheckOut"].HeaderText = "Giờ ra"; // Rename CheckOut column
            }
            else
            {
                MessageBox.Show("No attendance records found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void LoadData(List<Attendance> attendanceList)
        {
            if (attendanceList != null && attendanceList.Count > 0)
            {
                dataGridView1.DataSource = attendanceList;
                dataGridView1.Columns["Id"].Visible = false; // Hide Id column
                dataGridView1.Columns["UserId"].HeaderText = "Mã nhân viên"; // Rename UserId column
                dataGridView1.Columns["Date"].HeaderText = "Ngày"; // Rename Date column
                dataGridView1.Columns["CheckIn"].HeaderText = "Giờ vào"; // Rename CheckIn column
                dataGridView1.Columns["CheckOut"].HeaderText = "Giờ ra"; // Rename CheckOut column
            }
            else
            {
                MessageBox.Show("Không tìm thấy bản ghi chấm công nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ucSalary1_Load(object sender, EventArgs e)
        {

        }

        private void theoThờiGianCheckInToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedRowCount = (int) dataGridView1.SelectedRows[0].Cells["Id"].Value;

            if (selectedRowCount > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    attendanceRepository.DeleteAttendance(selectedRowCount);
                    LoadData();
                    MessageBox.Show("Đã xóa thành công bản ghi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bản ghi để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            Attendance attendance = new Attendance
            {
                UserId = Convert.ToInt32(txtUserID.Text),
                Date = DateTime.Now,
                CheckIn = dtpCheckIn.Value.TimeOfDay,
                CheckOut = dtpCheckOut.Value.TimeOfDay
            };
            if (attendanceRepository.AddAttendance(attendance))
            {
                MessageBox.Show("Chấm công thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                this.lblAttendanceToday.Text = "Đã chấm công ngày (" + DateTime.Now.ToString("dd/MM/yyyy") + "): " + attendanceRepository.CountAttendanceToday();
            }
            else
            {
                MessageBox.Show("Chấm công thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chấmCôngTheoUserIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Attendance> attendanceList = attendanceRepository.GetAttendanceByUserId(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["UserId"].Value));
            LoadData(attendanceList);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadData();
            page = 1; // Reset page when refreshing data
        }

        private void xuấtFileExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileExcelSelectedDataInDataGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileExcel();
        }

        private void SaveFileExcel()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls",
                Title = "Save Attendance Data"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Assuming you have a method to export data to Excel
                    attendanceRepository.ExportToExcel(saveFileDialog.FileName, dataGridView1.DataSource as List<Attendance>);
                    MessageBox.Show("Dữ liệu đã được xuất thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveFileExcelSelectedDataInDataGridView()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls",
                Title = "Save Selected Attendance Data"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<Attendance> selectedData = dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                        .Select(row => row.DataBoundItem as Attendance).ToList();
                    attendanceRepository.ExportToExcel(saveFileDialog.FileName, selectedData);
                    MessageBox.Show("Dữ liệu đã được xuất thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void sắpXếpTheoNgàyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var data = dataGridView1.DataSource as List<Attendance>;

            if (data == null) return;

            // Sort the data by Date in ascending order
            var newData = data.OrderBy(a => a.CheckIn).ToList();
            LoadData(newData);

        }

        private void sắpXếpTheoGiờVàoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var data = dataGridView1.DataSource as List<Attendance>;

            if (data == null) return;

            // Sort the data by Date in ascending order
            var newData = data.OrderBy(a => a.CheckIn).ToList();
            LoadData(newData);
        }

        private void sắpXếpTheoMãNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var data = dataGridView1.DataSource as List<Attendance>;

            if (data == null) return;

            // Sort the data by Date in ascending order
            var newData = data.OrderBy(a => a.UserId).ToList();
            LoadData(newData);

        }

        private void hiểnThịThêmDữLiệuCũHơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            page++;
            List<Attendance> attendanceList = attendanceRepository.GetAllAttendanceOlderThan(page);
            if (attendanceList != null && attendanceList.Count > 0)
            {
                LoadData(attendanceList);
            }
            else
            {
                MessageBox.Show("Không còn dữ liệu cũ hơn để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void hiểnThịDữLiệuMớiHơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            page--;
            List<Attendance> attendanceList = attendanceRepository.GetAllAttendanceOlderThan(page);
            if (attendanceList != null && attendanceList.Count > 0)
            {
                LoadData(attendanceList);
            }
            else
            {
                MessageBox.Show("Không còn dữ liệu mới hơn để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
