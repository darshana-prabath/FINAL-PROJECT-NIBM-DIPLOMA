using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using FINAL_PROJECT_NIBM.App_Code.Data;

namespace FINAL_PROJECT_NIBM
{
    public partial class users : Form
    {
        adminMainForm admin = new adminMainForm();
        UsersService uService = new UsersService();
        private String user_id;

        public users()
        {
            InitializeComponent();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {

            try
            {
                if (cmbtype.SelectedItem.ToString()==""|| txtname.Text==""||txtpw.Text=="")
                {
                    MessageBox.Show("MISSING INFORMATION", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    CREATE_USER();
                    READ_USER();
                    MessageBox.Show("USER SUCESSFULLY ADDED!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CLEAR();
                }
            }
            catch(Exception ex)
            {
                throw ex;
                MessageBox.Show("SOMETHING WENT WRONG!!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void users_Load(object sender, EventArgs e)
        {
            READ_USER();
        }

        private void DGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            user_id = DGV1.SelectedRows[0].Cells[0].Value.ToString();
            cmbtype.SelectedItem= DGV1.SelectedRows[0].Cells[1].Value.ToString();
            txtname.Text = DGV1.SelectedRows[0].Cells[2].Value.ToString();
            txtpw.Text = DGV1.SelectedRows[0].Cells[3].Value.ToString();
            txtgroup.Text = DGV1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {

            try
            {
                if (user_id == "")
                {
                    MessageBox.Show("ENTER YOUR USER ID!!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DELETE_USER();
                    READ_USER();
                    CLEAR();
                    MessageBox.Show("RECORD DELETED!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                throw ex;
                MessageBox.Show("RECORD DELETED FAILD!!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {

            try
            {
                if (user_id == "" || txtname.Text == "" || txtpw.Text == "" || txtgroup.Text=="")

                {
                    MessageBox.Show("MISSING DATA!!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    UPDATE_USER();
                    READ_USER();
                    CLEAR();
                    MessageBox.Show("RECORD UPDATED!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                throw ex;
                MessageBox.Show("RECORD UPDATED FAILD!!","Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            CLEAR();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Close();
            admin.Show();
        }

        private void btnhome_Click(object sender, EventArgs e)
        {
           
            admin.Show();
            this.Hide();
        }


        public void CREATE_USER()
        {
            uService.user_type = cmbtype.Text;
            uService.user_name = txtname.Text;
            uService.password = txtpw.Text;
            uService.group_id = txtgroup.Text;
            uService.Create_user();
        }

        public void READ_USER()
        {
            DGV1.DataSource = null;
            uService.Read_user();
            DGV1.DataSource = uService.dt;
        }

        public void UPDATE_USER()
        {
            uService.user_id = user_id;
            uService.user_name = txtname.Text;
            uService.password = txtpw.Text;
            uService.group_id = txtgroup.Text;
            uService.Update_user();
        }

        public void CLEAR()
        {
            cmbtype.SelectedIndex = 0;
            txtname.Text = "";
            txtpw.Text = "";
            txtgroup.Text = "";
            txtname.Focus();
        }

        public void DELETE_USER()
        {
            uService.user_id = user_id;
            uService.Delete_user();
        }


    }
    
}
