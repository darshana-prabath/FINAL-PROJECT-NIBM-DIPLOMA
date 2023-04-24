using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FINAL_PROJECT_NIBM.App_Code.Data;

namespace FINAL_PROJECT_NIBM
{
    public partial class FormProjectsQs : Form
    {
        ProjectService pService = new ProjectService();
        CalculateService cService = new CalculateService();
        private String id;
        public static int projectId;
        public FormProjectsQs()
        {
            InitializeComponent();
        }

        private void FormProjectsQs_Load(object sender, EventArgs e)
        {
            READ_PROJECT();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtProjectName.Text == "" || txtDescription.Text == "" || txtClient.Text == "" || cmbStatus.SelectedItem.ToString() == "" )
                {
                    MessageBox.Show("MISSING INFORMATION", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    CREATE_PROJECT();
                    //CREATE_SAMPLE_CALCULATION();
                    READ_PROJECT();
                    CLEAR();
                    MessageBox.Show("RECORD SUCESSFULLY ADDED!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                throw ex;
                MessageBox.Show("SOMETHING WENT WRONG!!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


           
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProjectName.Text == "" || txtDescription.Text == "" || txtClient.Text == "" || cmbStatus.SelectedItem.ToString() == "")

                {
                    MessageBox.Show("MISSING INFORMATION !!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    UPDATE_PROJECT();
                    READ_PROJECT();
                    CLEAR();
                    MessageBox.Show("RECORD UPDATED!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                MessageBox.Show("RECORD UPDATED FAILD!!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnclear_Click(object sender, EventArgs e)
        {
            CLEAR();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProjectName.Text == "")
                {
                    MessageBox.Show("MISSING INFORMATON!!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DELETE_PROJECT();
                    READ_PROJECT();
                    CLEAR();
                    MessageBox.Show("RECORD DELETED!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                MessageBox.Show("RECORD DELETED FAILD!!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void CREATE_PROJECT()
        {

            pService.ProjectName = txtProjectName.Text;
            pService.Description = txtDescription.Text;
            pService.Client = txtClient.Text;
            pService.Date = dtDate.Value.Date.ToString("yyyyMMdd");
            pService.Status = cmbStatus.Text;
            pService.Create_project();
        }

        private void CREATE_SAMPLE_CALCULATION()
        {
            string[] stringarr = new string[11] { "Concrete Amount", "Reinforcement Amount", "Plaster Amount", "Paint Amount", "Tiling Amount", "Formwork Amount", "Waterproofing Amount", "Door window Amount", "Sanitary fitting Amount", "Pantry Amount", "Plumbing Amount" };


            for (int i = 0; i < stringarr.Length; i++)
            {
                cService.Type = stringarr[i];
                cService.Unit = "0";
                cService.Quantity = "0";
                cService.Rate = "0";
                cService.Amount = "0";
                cService.Create_Sample_calculate();
        }

    }

        private void READ_PROJECT()
        {
            DGV2.DataSource = null;
            pService.Read_project();
            DGV2.DataSource = pService.dt;
        }

        private void UPDATE_PROJECT()
        {
            pService.id = id;
            pService.ProjectName = txtProjectName.Text;
            pService.Description = txtDescription.Text;
            pService.Client = txtClient.Text;
            pService.Date = dtDate.Value.Date.ToString("yyyyMMdd");
            pService.Status = cmbStatus.Text;
            pService.Update_project();
        }

        private void CLEAR()
        {
            txtProjectName.Text = "";
            txtDescription.Text = "";
            txtClient.Text = "";
            cmbStatus.Text = "";
            txtProjectName.Focus();
        }

        private void DELETE_PROJECT()
        {
            pService.id = id;
            pService.Delete_project();
        }

        private void DGV2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = DGV2.SelectedRows[0].Cells[0].Value.ToString();
            txtProjectName.Text = DGV2.SelectedRows[0].Cells[1].Value.ToString();
            txtDescription.Text = DGV2.SelectedRows[0].Cells[2].Value.ToString();
            dtDate.Text = DGV2.SelectedRows[0].Cells[3].Value.ToString();
            txtClient.Text = DGV2.SelectedRows[0].Cells[4].Value.ToString();
            cmbStatus.SelectedItem = DGV2.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DGV2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (DGV2.SelectedRows[0] != null)
            {
                projectId = int.Parse(DGV2.SelectedRows[0].Cells[0].Value.ToString());

                DialogResult dr = MessageBox.Show("Are you want to create new calculations?", "Project", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:

                        calculate frmcalculate = new calculate();
                        this.Hide();
                        frmcalculate.Show();

                        break;
                    case DialogResult.No:
                        break;
                }
            }

            
        }

    }
}
