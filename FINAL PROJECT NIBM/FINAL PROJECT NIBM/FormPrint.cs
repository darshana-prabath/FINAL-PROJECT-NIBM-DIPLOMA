using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;

namespace FINAL_PROJECT_NIBM
{
    public partial class FormPrint : Form
    {
        ReportDocument rptDoc = new ReportDocument();
        DataSet ds = new DataSet();
        private int project_id;
        public FormPrint()
        {
            InitializeComponent();
        }

        private void FormPrint_Load(object sender, EventArgs e)
        {
            LoadCitizenshipReport(calculate.projectId);

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            //LoadCitizenshipReport(int ? ID);
        }

        public void LoadCitizenshipReport(int ID)
        {

            SqlConnection Con = new SqlConnection("Data Source=DESKTOP-ITEQDHG;Initial Catalog=QSestimateDB;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();

            Con.Open();




            adapter = new SqlDataAdapter("select Description, Amount from final_view ", Con);
            adapter.Fill(ds);
            rptDoc.Load(@"../../Report/CrystalReport.rpt");
            rptDoc.SetDataSource(ds.Tables[0]);
            crystalReportViewer1.ReportSource = rptDoc;




        }

        private void button1_Click(object sender, EventArgs e)
        {
            rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, @"E:\Estimates.pdf");

            MessageBox.Show("Exported Successful");
        }


        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
