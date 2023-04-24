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
    public partial class AddPdf : Form
    {
        public AddPdf()
        {
            InitializeComponent();
        }

        private void AddPdf_Load(object sender, EventArgs e)
        {

        }

        private void btnopen_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                axAcroPDF1.src = open.FileName;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            adminMainForm admin = new adminMainForm();
            admin.Show();
            this.Hide();
        }
    }
}
