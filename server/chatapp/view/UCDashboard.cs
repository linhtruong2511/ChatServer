using chatapp.context;
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

namespace chatapp
{
    public partial class UCDashboard : UserControl
    {
        private UserRepository userRepository;
        private DepartmentRepository departmentRepository;
        private SalaryRepository salaryRepository;
        private ProjectRepository projectRepository;
        private TaskRepository taskRepository;
        private AttendanceRepository attendanceRepository;
        

        public UCDashboard()
        {
            InitializeComponent();

            userRepository = new UserRepository();
            departmentRepository = new DepartmentRepository();
            salaryRepository = new SalaryRepository();
            projectRepository = new ProjectRepository();
            taskRepository = new TaskRepository();
            attendanceRepository = new AttendanceRepository();
        }

        private void UCDashboard_Load(object sender, EventArgs e)
        {
            if (Database.GetConnection() != null)
            {
                this.lblUserCount.Text = userRepository.CountUser().ToString();
                this.lblDepartmentCount.Text = new DepartmentRepository().CountDepartment().ToString();
                this.lblMinSalary.Text = salaryRepository.MinSalary().ToString("C0");
                this.lblMaxSalary.Text = salaryRepository.MaxSalary().ToString("C0");
                this.lblProjectCount.Text = projectRepository.CountProject().ToString();
                this.lblAverageSalary.Text = salaryRepository.AverageSalary().ToString("C0");
                this.lblProjectCountUnFinished.Text = projectRepository.countUnfinishedProject().ToString();
                this.lblProjectCountFinished.Text = projectRepository.countFinishedProject().ToString();   
                this.lblCountTask.Text = taskRepository.CountTask().ToString();
                this.lblCountAttendance.Text = attendanceRepository.CountAttendance().ToString(); 
            }
        }

        public async void ShowAction(string action)
        {
            while (!textBox1.IsHandleCreated)
            {
                await Task.Delay(10); // đợi đến khi handle được tạo
            }

            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new Action(() =>
                {
                    textBox1.Text += $"{DateTime.Now:HH:mm:ss} - {action}\r\n";
                }));
            }
            else
            {
                textBox1.Text += $"{DateTime.Now:HH:mm:ss} - {action}\r\n";
            }
        }
    }
}
