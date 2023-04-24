using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.Data.SqlClient;
using FINAL_PROJECT_NIBM.App_Code.Data;


namespace FINAL_PROJECT_NIBM
{
    public partial class Output : Form
    {
        public static string projectId = "";
        public Output()
        {
            InitializeComponent();
        }

        DBUtils dbutils = new DBUtils();
       
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-ITEQDHG;Initial Catalog=QSestimateDB;Integrated Security=True");

        private void Output_Load(object sender, EventArgs e)
        {

        }

        private void btnhome_Click(object sender, EventArgs e)
        {
             MainForm home = new MainForm();
            home.Show();
            this.Hide();
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            //workbook.Worksheets.Add(this.finalViewDataSet.final_view.CopyToDataTable(), "quantity_deta");
                            workbook.SaveAs(sfd.FileName);
                        }
                        MessageBox.Show("you have sucessfully exported your data to an excel file ", "message", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            //workbook.Worksheets.Add(this.finalViewDataSet.final_view.CopyToDataTable(), "quantity_deta");
                            workbook.SaveAs(sfd.FileName);
                        }
                        MessageBox.Show("you have sucessfully exported your data to an excel file ", "message", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnPDF_Click_1(object sender, EventArgs e)
        {
            FormPrint frmPrint = new FormPrint();
            frmPrint.Show();
        }

        private void DGV2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
