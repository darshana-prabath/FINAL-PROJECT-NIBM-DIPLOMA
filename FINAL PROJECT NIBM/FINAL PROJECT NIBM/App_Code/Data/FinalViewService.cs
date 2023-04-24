using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FINAL_PROJECT_NIBM.App_Code.Data
{
    class FinalViewService : DBUtils
    {
        //PROPERTIES
        public string Description { set; get; }
        public Int64 Amount { set; get; }
        public string id { set; get; }
        public int project_id { set; get; }

        //READ PROPERTIES

        public DataTable dt = new DataTable();
        private DataSet ds = new DataSet();

        //CREATE FUNCTION
        public void Create_final_view()
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                Int32 count;
                SqlCommand com = new SqlCommand("SELECT Top 1 id FROM final_view Order by id desc ", con);
                count = Convert.ToInt32(com.ExecuteScalar());
                if (count < 0)
                {
                    count = 1;
                }
                else
                {
                    count += 1;
                }

                cmd.CommandText = "INSERT INTO final_view(id, Description,Amount,project_id) VALUES(@id, @Description, @Amount, @project_id ) ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = count;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = Description;
                cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = Amount;
                cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = project_id;

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //READ FUNCTION
        public void Read_calculate()
        {
            dt.Clear();
            string query = "SELECT * from final_view";
            //string query = "SELECT * from quantity_data WHERE id=@project_id ";
            //con.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd.Parameters.Add("@project_id", SqlDbType.VarChar).Value = project_id;
            //cmd.ExecuteNonQuery();
            SqlDataAdapter MDA = new SqlDataAdapter(query, con);

            MDA.Fill(ds);
            dt = ds.Tables[0];

        }



       
    }
}
