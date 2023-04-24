using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FINAL_PROJECT_NIBM.App_Code.Data
{
    class UsersService : DBUtils
    {

        //PROPERTIES

        public string user_id { set; get; }
        public string user_type { set; get; }
        public string user_name { set; get; }
        public string password { set; get; }
        public string group_id { set; get; }


        //READ PROPERTIES

        public DataTable dt = new DataTable();
        private DataSet ds = new DataSet();

        //CREATE FUNCTION
        public void Create_user()
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                Int32 count;
                SqlCommand com = new SqlCommand("SELECT Top 1 user_id FROM login Order by user_id desc ", con);
                count = Convert.ToInt32(com.ExecuteScalar());
                if (count < 0)
                {
                    count = 1;
                }
                else
                {
                    count += 1;
                }

                cmd.CommandText = "INSERT INTO login( user_id, user_type, user_name, password, group_id) VALUES(@user_id, @user_type, @user_name, @password, @group_id) ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@user_id", SqlDbType.VarChar).Value = count;
                cmd.Parameters.Add("@user_type", SqlDbType.VarChar).Value = user_type;
                cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = user_name;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
                cmd.Parameters.Add("@group_id", SqlDbType.VarChar).Value = group_id;

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //READ FUNCTION
        public void Read_user()
        {
            dt.Clear();
            string query = "SELECT * from login ";
            SqlDataAdapter MDA = new SqlDataAdapter(query, con);
            MDA.Fill(ds);
            dt = ds.Tables[0];

        }

        //UPDATE FUNCTION
        public void Update_user()
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "UPDATE login SET user_name=@user_name, password=@password, group_id=@group_id  WHERE user_id=@user_id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = user_name;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
                cmd.Parameters.Add("@group_id", SqlDbType.VarChar).Value = group_id;

                cmd.Parameters.Add("@user_id", SqlDbType.VarChar).Value = user_id;

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        //DELETE FUNCTION
        public void Delete_user()
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "DELETE FROM login WHERE user_id=@user_id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@user_id", SqlDbType.VarChar).Value = user_id;

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


    }
}
