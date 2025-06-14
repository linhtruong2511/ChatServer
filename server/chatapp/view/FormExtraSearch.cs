using chatapp.model;
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
    public partial class FormExtraSearch : Form
    {
        public User User { get; set; } = new User();
        public FormExtraSearch()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            if (txtName.Text != string.Empty)
            {
                User.Name = txtName.Text;
            }
            else
            {
                User.Name = null;
            }

            if (txtPhone.Text != string.Empty)
            {
                User.Phone = txtPhone.Text;
            }
            else
            {
                User.Phone = null;
            }

            if (txtAddress.Text != string.Empty)
            {
                User.Address = txtAddress.Text;
            }
            else
            {
                User.Address = null;
            }

            if (txtUsername.Text != string.Empty)
            {
                User.Username = txtUsername.Text;
            }
            else
            {
                User.Username = null;
            }

            int deartmentId = -1;
            int positionId = -1;

            try
            {
                deartmentId = int.Parse(txtDepartmentId.Text);
                positionId = int.Parse(txtPositionId.Text);
            } catch (Exception) { }

            if (deartmentId > 0)
            {
                User.DepartmentId = deartmentId;
            }
            else
            {
                User.DepartmentId = -1;
            }

            if (positionId > 0)
            {
                User.PositionId = positionId;
            }
            else
            {
                User.PositionId = -1;
            }
            
            this.Close();
        }
    }
}
