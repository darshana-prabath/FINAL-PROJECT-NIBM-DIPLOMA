using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FINAL_PROJECT_NIBM.App_Code.Data
{
    class ProjectService : DBUtils
    {
        //PROPERTIES

        public string ProjectName { set; get; }
        public string Description { set; get; }
        public string Date { set; get; }
        public string Client { set; get; }
        public string Status { set; get; }
        public string id { set; get; }


        //READ PROPERTIES

        public DataTable dt = new DataTable();
        private DataSet ds = new DataSet();

        //CREATE FUNCTION
        public void Create_project()
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                Int32 count;
                SqlCommand com = new SqlCommand("SELECT Top 1 id FROM projects Order by id desc ", con);
                count = Convert.ToInt32(com.ExecuteScalar());
                if (count < 0)
                {
                    count = 1;
                }
                else
                {
                    count += 1;
                }

                cmd.CommandText = "INSERT INTO projects(id, ProjectName,Description,Date,Client,Status) VALUES(@id, @ProjectName, @Description, @Date, @Client, @Status) ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = count;
                cmd.Parameters.Add("@ProjectName", SqlDbType.VarChar).Value = ProjectName;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = Description;
                cmd.Parameters.Add("@Date", SqlDbType.VarChar).Value = Date;
                cmd.Parameters.Add("@Client", SqlDbType.VarChar).Value = Client;
                cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status;

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //READ FUNCTION
        public void Read_project()
        {
            dt.Clear();
            string query = "SELECT * from projects ";
            SqlDataAdapter MDA = new SqlDataAdapter(query, con);
            MDA.Fill(ds);
            dt = ds.Tables[0];

        }

        //UPDATE FUNCTION
        public void Update_project()
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "UPDATE projects SET ProjectName=@ProjectName, Description=@Description, Date=@Date, Client=@Client, Status=@Status  WHERE id=@id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@ProjectName", SqlDbType.VarChar).Value = ProjectName;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = Description;
                cmd.Parameters.Add("@Date", SqlDbType.VarChar).Value = Date;
                cmd.Parameters.Add("@Client", SqlDbType.VarChar).Value = Client;
                cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status;

                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        //DELETE FUNCTION
        public void Delete_project()
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                Int32 count;
                SqlCommand com = new SqlCommand("SELECT Top 1 id FROM projects Order by id desc ", con);
                count = Convert.ToInt32(com.ExecuteScalar());
                if (count < 0)
                {
                    count = 1;
                }
                else
                {
                    count += 1;
                }

                cmd.CommandText = "DELETE FROM projects WHERE id=@id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = count;

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
