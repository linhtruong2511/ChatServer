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
            userRepository = new UserRepository();
            departmentRepository = new DepartmentRepository();
            salaryRepository = new SalaryRepository();
            projectRepository = new ProjectRepository();
            taskRepository = new TaskRepository();
            attendanceRepository = new AttendanceRepository();
            InitializeComponent();
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
                this.W.Text = projectRepository.countUnfinishedProject().ToString();
                this.lblProjectCountFinished.Text = projectRepository.countFinishedProject().ToString();   
                this.lblCountTask.Text = taskRepository.CountTask().ToString();
                this.lblCountAttendance.Text = attendanceRepository.CountAttendance().ToString(); 

            }
        }

        public void ShowAction(string action)
        {  
            textBox1.Text+= $"{DateTime.Now.ToString("HH:mm:ss")} - {action}\r\n";
        }
    }
}
