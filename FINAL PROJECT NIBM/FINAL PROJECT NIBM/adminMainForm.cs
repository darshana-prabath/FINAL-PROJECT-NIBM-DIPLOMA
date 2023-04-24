using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FINAL_PROJECT_NIBM
{
    public partial class adminMainForm : Form
    {
        public adminMainForm()
        {
            InitializeComponent();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            users user = new users();
            user.Show();
            this.Hide();
        }

        private void adminMainForm_Load(object sender, EventArgs e)
        {
           
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {

            AddPdf add = new AddPdf();
            add.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            login lo = new login();
            lo.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Close();
            login frmlogin = new login();
            frmlogin.Show();
        }
    }
}
