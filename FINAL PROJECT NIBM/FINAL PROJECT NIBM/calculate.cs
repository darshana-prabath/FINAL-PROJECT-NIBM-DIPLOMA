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
    public partial class calculate : Form
    {
        CalculateService cService = new CalculateService();
        FinalViewService fService = new FinalViewService();
        private String id;
        private int project_id;
        private int Check_project_id;
        public static int projectId;

        public calculate()
        {
            InitializeComponent();
        }
        private void btnamount_Click(object sender, EventArgs e)
        {
            
            
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmbtype.SelectedItem.ToString() == "" || cmbunit.SelectedItem.ToString() == "" || txtqua.Text == "" || txtrate.Text == "")
                {
                    MessageBox.Show("MISSING INFORMATION", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    CREATE_CALCULATE();
                    READ_CALCULATE();
                    CLEAR();
                    MessageBox.Show("USER SUCESSFULLY ADDED!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (cmbtype.SelectedIndex.ToString() == "" || cmbunit.SelectedIndex.ToString() == "" || txtqua.Text == "" || txtrate.Text == "")

                {
                    MessageBox.Show("MISSING INFORMATION !!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    UPDATE_CALCULATE();
                    READ_CALCULATE();
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

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbtype.SelectedIndex.ToString()== "")
                {
                    MessageBox.Show("MISSING INFORMATON!!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DELETE_CALCULATE();
                    READ_CALCULATE();
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

        private void btnhome_Click(object sender, EventArgs e)
        {
            MainForm home = new MainForm();
            home.Show();
            this.Hide();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            CLEAR();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DGV2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = DGV2.SelectedRows[0].Cells[0].Value.ToString();
            cmbtype.SelectedItem= DGV2.SelectedRows[0].Cells[1].Value.ToString();
           cmbunit.SelectedItem = DGV2.SelectedRows[0].Cells[2].Value.ToString();
            txtqua.Text = DGV2.SelectedRows[0].Cells[3].Value.ToString();
           txtrate.Text = DGV2.SelectedRows[0].Cells[4].Value.ToString();
           txtamount.Text = DGV2.SelectedRows[0].Cells[5].Value.ToString();
        }
        double amount;
        private void btnamount_Click_1(object sender, EventArgs e)
        {
          
            int j = cmbtype.SelectedIndex;

            for (j = 0; j <= 4; j++)
            {
                double rate = Convert.ToDouble(txtrate.Text);
                double qua = Convert.ToDouble(txtqua.Text);
                amount = rate * qua;
                txtamount.Text = amount.ToString();
            }
        }

        private void calculate_Load(object sender, EventArgs e)
        {
            project_id = FormProjectsQs.projectId;
            projectId = project_id;
            READ_CALCULATE();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            project_id = FormProjectsQs.projectId;
            projectId = project_id;
            READ_PROJECT_ID();

        }

        private void cmbtype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /* private void btntotal_Click(object sender, EventArgs e)
         {
             double tot= 0;

            if(cmbtype.SelectedIndex==3)
             {
                 tot = tot + amount;
                 txttotal.Text = tot.ToString();

             }
         }*/


        public void CREATE_CALCULATE()
        {
            
            cService.Type = cmbtype.Text;
            cService.Unit = cmbunit.Text;
            cService.Quantity = txtqua.Text;
            cService.Rate = txtrate.Text;
            cService.Amount = txtamount.Text;
            cService.project_id = project_id;
            cService.Create_calculate();
        }

        public void READ_CALCULATE()
        {
            DGV2.DataSource = null;
            cService.project_id = project_id;
            cService.Read_calculate(project_id);
            DGV2.DataSource = cService.dt;
        }

        public void READ_CONCRETE_AMOUNT()
        {
            DGV2.DataSource = null;
            cService.project_id = project_id;
            cService.Get_ConcreteAmountType(project_id);
            Int64 ConcreteAmount = cService.ConcreteAmount;
            fService.Description = "Concrete Amount";
            fService.Amount = ConcreteAmount;
            fService.project_id = project_id;
            fService.Create_final_view();
        }

        //2
        public void READ_RAINFORCEMENT_AMOUNT()
        {
            DGV2.DataSource = null;
            cService.project_id = project_id;
            cService.Get_ReinforcementAmountType(project_id);
            Int64 ReinforcementAmount = cService.ReinforcementAmount;
            fService.Description = "Reinforcement Amount";
            fService.Amount = ReinforcementAmount;
            fService.project_id = project_id;
            fService.Create_final_view();
        }

        //3
        public void READ_PLASTER_AMOUNT()
        {
            DGV2.DataSource = null;
            cService.project_id = project_id;
            cService.Get_PlasterAmountType(project_id);
            Int64 PlasterAmount = cService.PlasterAmount;
            fService.Description = "Plaster Amount";
            fService.Amount = PlasterAmount;
            fService.project_id = project_id;
            fService.Create_final_view();
        }

        //4
        public void READ_PAINT_AMOUNT()
        {
            DGV2.DataSource = null;
            cService.project_id = project_id;
            cService.Get_PaintAmountType(project_id);
            Int64 PaintAmount = cService.PaintAmount;
            fService.Description = "Paint Amount";
            fService.Amount = PaintAmount;
            fService.project_id = project_id;
            fService.Create_final_view();
        }

        //5
        public void READ_TILING_AMOUNT()
        {
            DGV2.DataSource = null;
            cService.project_id = project_id;
            cService.Get_TilingAmountType(project_id);
            Int64 TilingAmount = cService.TilingAmount;
            fService.Description = "Tiling Amount";
            fService.Amount = TilingAmount;
            fService.project_id = project_id;
            fService.Create_final_view();
        }

        //6
        public void READ_FORMWORK_AMOUNT()
        {
            DGV2.DataSource = null;
            cService.project_id = project_id;
            cService.Get_FormworkAmountType(project_id);
            Int64 FormworkAmount = cService.FormworkAmount;
            fService.Description = "Formwork Amount";
            fService.Amount = FormworkAmount;
            fService.project_id = project_id;
            fService.Create_final_view();
        }

        //7
        public void READ_WATERPROOFING_AMOUNT()
        {
            DGV2.DataSource = null;
            cService.project_id = project_id;
            cService.Get_WaterproofingAmountType(project_id);
            Int64 WaterproofingAmount = cService.WaterproofingAmount;
            fService.Description = "Waterproofing Amount";
            fService.Amount = WaterproofingAmount;
            fService.project_id = project_id;
            fService.Create_final_view();
        }

        //8
        public void READ_DOORWINDOW_AMOUNT()
        {
            DGV2.DataSource = null;
            cService.project_id = project_id;
            cService.Get_DoorwindowAmountType(project_id);
            Int64 DoorwindowAmount = cService.DoorwindowAmount;
            fService.Description = "Door window Amount";
            fService.Amount = DoorwindowAmount;
            fService.project_id = project_id;
            fService.Create_final_view();
        }

        //9
        public void READ_SANITARYFITTING_AMOUNT()
        {
            DGV2.DataSource = null;
            cService.project_id = project_id;
            cService.Get_SanitaryfittingAmountType(project_id);
            Int64 SanitaryfittingAmount = cService.SanitaryfittingAmount;
            fService.Description = "Sanitary fitting Amount";
            fService.Amount = SanitaryfittingAmount;
            fService.project_id = project_id;
            fService.Create_final_view();
        }

        //10
        public void READ_PANTRY_AMOUNT()
        {
            DGV2.DataSource = null;
            cService.project_id = project_id;
            cService.Get_PantryAmountType(project_id);
            Int64 PantryAmount = cService.PantryAmount;
            fService.Description = "Pantry Amount";
            fService.Amount = PantryAmount;
            fService.project_id = project_id;
            fService.Create_final_view();
        }

        //11
        public void READ_PLUMBING_AMOUNT()
        {
            DGV2.DataSource = null;
            cService.project_id = project_id;
            cService.Get_PlumbingAmountType(project_id);
            Int64 PlumbingAmount = cService.PlumbingAmount;
            fService.Description = "Plumbing Amount";
            fService.Amount = PlumbingAmount;
            fService.project_id = project_id;
            fService.Create_final_view();
        }

        

             //Read Project ID
        public void READ_PROJECT_ID()
        {
            DGV2.DataSource = null;
            cService.project_id = project_id;
            cService.Get_ProjectID();
            Check_project_id = cService.project_id;

            if(Check_project_id.Equals(project_id))
            {
                FormPrint opt = new FormPrint();
                opt.Show();
                this.Hide();
            }
            else
            {
                READ_CONCRETE_AMOUNT();
                READ_RAINFORCEMENT_AMOUNT();
                READ_PLASTER_AMOUNT();
                READ_PAINT_AMOUNT();
                READ_TILING_AMOUNT();
                READ_FORMWORK_AMOUNT();
                READ_WATERPROOFING_AMOUNT();
                READ_DOORWINDOW_AMOUNT();
                READ_SANITARYFITTING_AMOUNT();
                READ_PANTRY_AMOUNT();
                READ_PLUMBING_AMOUNT();

                FormPrint opt = new FormPrint();
                opt.Show();
                this.Hide();
            }
        }

        public void UPDATE_CALCULATE()
        {
            cService.id = id;
            cService.Type = cmbtype.Text;
            cService.Unit = cmbunit.Text;
            cService.Quantity = txtqua.Text;
            cService.Rate = txtrate.Text;
            cService.Amount = txtamount.Text;
            cService.Update_calculate();
        }

        public void CLEAR()
        {
            cmbtype.SelectedIndex = 0;
            cmbunit.SelectedIndex = 0;
            txtqua.Text = "";
            txtrate.Text = "";
            txtamount.Text = "";
            cmbtype.Focus();
        }

        public void DELETE_CALCULATE()
        {
            cService.id = id;
            cService.Delete_calculate();
        }

        private void txtqua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtrate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
