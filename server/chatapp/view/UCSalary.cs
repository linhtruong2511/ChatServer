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
    public partial class UCSalary : UserControl
    {
        private int month = DateTime.Now.Month; // Get the current month
        private int year = DateTime.Now.Year; // Get the current year
        SalaryRepository salaryRepository = new SalaryRepository();  
        public UCSalary()
        {
            InitializeComponent();
        }

        private void UCSalary_Load(object sender, EventArgs e)
        {
            LoadData(); // Load data when the control is loaded
            lblDate.Text = "Tháng: " + month.ToString() + "/" + year.ToString();
        }

        private void LoadData()
        {
            List<Salary> salaries = salaryRepository.getAllSalariesInMonthAndYear(month, year); // Assuming you have a method to get all salaries
            dataGridView1.DataSource = null; // Clear any existing data
            dataGridView1.DataSource = salaries; // Bind the data to the DataGridView
            setHeaderText();
        }

        private void LoadData(int month, int year)
        {
            this.month = month; // Update the month
            this.year = year; // Update the year
            LoadData(); // Reload data for the specified month
        }

        private void LoadData(List<Salary> salaries)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = salaries;
            setHeaderText();
        }

        private void setHeaderText()
        {
            dataGridView1.Columns["Id"].Visible = false; // Hide Id column if not needed
            dataGridView1.Columns["UserID"].HeaderText = "Mã nhân viên"; // Rename UserID column
            dataGridView1.Columns["Base_Salary"].HeaderText = "Lương cơ bản"; // Rename Base_Salary column
            dataGridView1.Columns["Bonus"].HeaderText = "Lương thưởng"; // Rename Bonus column
            dataGridView1.Columns["Date"].HeaderText = "Tháng lương"; // Rename Month column
            dataGridView1.Columns["Date"].DefaultCellStyle.Format = "MM/yyyy"; // Format Month column to show only month and year
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUserId.Text == "" || txtBaseSalary.Text == "" || txtBonus.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Salary salary = new Salary()
            {
                UserID = int.Parse(txtUserId.Text),
                Base_Salary = decimal.Parse(txtBaseSalary.Text),
                Bonus = decimal.Parse(txtBonus.Text),
                Date = dtpDate.Value
            };
            salaryRepository.AddSalary(salary); // Assuming you have a method to add a salary
            MessageBox.Show("Thêm lương thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData(); // Reload data after adding a new salary
        }

        private void sắpXếpTheoMãNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Salary> salaries = (List<Salary>)dataGridView1.DataSource;
            if (salaries != null)
            {
                var sortedSalaries = salaries.OrderBy(s => s.UserID).ToList(); // Sort by UserID
                LoadData(sortedSalaries); // Reload data with sorted salaries
            }
        }

        private void sắpXếpTheoLươngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void giảmDầnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Salary> salaries = (List<Salary>)dataGridView1.DataSource;
            if (salaries != null)
            {
                var sortedSalaries = salaries.OrderByDescending(s => s.Base_Salary).ToList(); // Sort by Base_Salary in descending order
                LoadData(sortedSalaries); // Reload data with sorted salaries
            }
        }

        private void tăngDầnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Salary> salaries = (List<Salary>)dataGridView1.DataSource;
            if(salaries != null)
            {
                var sortedSalaries = salaries.OrderBy(s => s.Base_Salary).ToList(); // Sort by Base_Salary in ascending order
                LoadData(sortedSalaries); // Reload data with sorted salaries
            }
        }

        private void sắpXếpTheoLươngThườngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Salary> salaries = (List<Salary>)dataGridView1.DataSource;
            if (salaries != null)
            {
                var sortedSalaries = salaries.OrderBy(s => s.Bonus).ToList(); // Sort by Bonus
                LoadData(sortedSalaries); // Reload data with sorted salaries
            }
        }

        private void xóaBảnGhiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int salaryId = dataGridView1.SelectedRows[0].Cells["Id"].Value != null ? Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value) : 0;
            if (salaryId > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    salaryRepository.DeleteSalary(salaryId); // Assuming you have a method to delete a salary
                    MessageBox.Show("Xóa bản ghi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Reload data after deletion
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bản ghi để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void giảmDầnToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            List<Salary> salaries = (List<Salary>)dataGridView1.DataSource;
            if (salaries != null)
            {
                var sortedSalaries = salaries.OrderByDescending(s => s.Bonus).ToList(); // Sort by Bonus in descending order
                LoadData(sortedSalaries); // Reload data with sorted salaries
            }
        }

        private void tăngDầnToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            List<Salary> salaries = (List<Salary>)dataGridView1.DataSource;
            if (salaries != null)
            {
                var sortedSalaries = salaries.OrderBy(s => s.Bonus).ToList(); // Sort by Bonus in ascending order
                LoadData(sortedSalaries); // Reload data with sorted salaries
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Salary> salaries = dataGridView1.DataSource as List<Salary>; // Get all salaries for the current month and year
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx|All Files|*.*",
                Title = "Save Salary Report"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Assuming you have a method to export salaries to Excel
                    salaryRepository.ExportSalariesToExcel(salaries, saveFileDialog.FileName);
                    MessageBox.Show("Báo cáo lương đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lưu báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lươngThángTrướcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.month--; // Decrement month by 1
            if (month < 1) // If month is less than January, reset to December and decrement year
            {
                this.month = 12;
                this.year--;
            }
            
            lblDate.Text = "Tháng: " + month.ToString() + "/" + year.ToString(); // Update label to show the new month and year
            List<Salary> salaries = salaryRepository.getAllSalariesInMonthAndYear(month, year); // Get all salaries for the previous month
            LoadData(salaries);
        }

        private void lươngThángSauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.month++; // Increment month by 1
            if (month > 12) // If month exceeds December, reset to January and increment year
            {
                this.month = 1;
                this.year++;
            }

            if (month > DateTime.Now.Month && year >= DateTime.Now.Year) // Prevent future months
            {
                MessageBox.Show("Không thể xem lương cho tháng tương lai!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.month--; // Revert month change
                return;
            }

            lblDate.Text = "Tháng: " + month.ToString() + "/" + year.ToString(); // Update label to show the new month and year
            List<Salary> salaries = salaryRepository.getAllSalariesInMonthAndYear(month, year); // Get all salaries for the next month
            LoadData(salaries); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Salary> salaries = salaryRepository.getAllSalariesInMonthAndYear(DateTime.Now.Month, DateTime.Now.Year); // Get all salaries for the current month and year
            LoadData(salaries); // Reload data for the current month
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    List<Salary> salaries = salaryRepository.getAllSalariesInMonthAndYear(month, year); // Get all salaries for the current month and year
        //    if (salaries != null)
        //    {
        //        decimal totalSalary = salaries.Sum(s => s.Base_Salary + s.Bonus); // Calculate total salary
        //        decimal averageSalary = totalSalary / salaries.Count; // Calculate average salary
        //        MessageBox.Show($"Tổng lương: {totalSalary:C}\nLương trung bình: {averageSalary:C}", "Thông tin lương", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Không có dữ liệu lương cho tháng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}
    }
}
