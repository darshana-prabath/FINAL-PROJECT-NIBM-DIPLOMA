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
    public partial class login : Form
    {
        DatabaseUtils DBUtils = new DatabaseUtils();
        SqlConnection sc = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader reader;

        public login()
        {
            InitializeComponent();
        }
      
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //String cons = DBUtils.DBConnectionStr;
        //SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-CNH07R85;Initial Catalog=QSestimateDB;Integrated Security=True");
        private void btnlogin_Click(object sender, EventArgs e)
        {
            userlogin();
     
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            cmbid.SelectedIndex = 0;
           txtuid .Text = "";
            txtpw.Text = "";
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void userlogin()
        {
            try
            {
                if (txtuid.Text.Length > 0 && txtpw.Text.Length > 0)
                {
                    String cons = DBUtils.DBConnectionStr;
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM login WHERE user_id='" + txtuid.Text + "' AND password='" + txtpw.Text + "'", cons);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (cmbid.Text.Equals("ADMIN")) adm();


                        if (cmbid.Text.Equals("QUANTITY SURVEOR")) qso();

                        void adm()
                        {
                            adminMainForm admin = new adminMainForm();
                            admin.Show();
                            this.Hide();
                            //con.Close();

                        }

                        void qso()
                        {
                            MainForm home = new MainForm();
                            home.Show();
                            this.Hide();
                            //con.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("WRONG USER NAME OR PASSWORD!!!");
                    }
                }
                else
                {
                    MessageBox.Show("Please fill username and password and try again", "Invalid Login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
