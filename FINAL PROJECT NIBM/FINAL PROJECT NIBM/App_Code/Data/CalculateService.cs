using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FINAL_PROJECT_NIBM.App_Code.Data
{
    class CalculateService : DBUtils
    {
        //PROPERTIES

        String ConcreteAmountType = "Concrete Amount";
        String ReinforcementAmountType = "Reinforcement Amount";
        String PlasterAmountType = "Plaster Amount";
        String PaintAmountType = "Paint Amount";
        String TilingAmountType = "Tiling Amount";
        String FormworkAmountType = "Formwork Amount";
        String WaterproofingAmountType = "Waterproofing Amount";
        String DoorwindowAmountType = "Door window Amount";
        String SanitaryfittingAmountType = "Sanitary fitting Amount";
        String PantryAmountType = "Pantry Amount";
        String PlumbingAmountType = "Plumbing Amount";

        public string Type { set; get; }
        public string Unit { set; get; }
        public string Quantity { set; get; }
        public string Rate { set; get; }
        public string Amount { set; get; }
        public string id { set; get; }
        public int project_id { set; get; }

        public Int64 ConcreteAmount { set; get; }
        public Int64 ReinforcementAmount { set; get; }
        public Int64 PlasterAmount { set; get; }
        public Int64 PaintAmount { set; get; }
        public Int64 TilingAmount { set; get; }
        public Int64 FormworkAmount { set; get; }
        public Int64 WaterproofingAmount { set; get; }
        public Int64 DoorwindowAmount { set; get; }
        public Int64 SanitaryfittingAmount { set; get; }
        public Int64 PantryAmount { set; get; }
        public Int64 PlumbingAmount { set; get; }


        //READ PROPERTIES

        public DataTable dt = new DataTable();
        private DataSet ds = new DataSet();

        //CREATE FUNCTION
        public void Create_calculate()
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                Int32 count;
                SqlCommand com = new SqlCommand("SELECT Top 1 id FROM quantity_data Order by id desc ", con);
                count = Convert.ToInt32(com.ExecuteScalar());
                if (count < 0)
                {
                    count = 1;
                }
                else
                {
                    count += 1;
                }

                cmd.CommandText = "INSERT INTO quantity_data(id, Type,Unit,Quantity,Rate,Amount,project_id) VALUES(@id, @Type, @Unit, @Quantity, @Rate, @Amount, @project_id ) ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = count;
                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = Type;
                cmd.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Unit;
                cmd.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = Quantity;
                cmd.Parameters.Add("@Rate", SqlDbType.VarChar).Value = Rate;
                cmd.Parameters.Add("@Amount", SqlDbType.VarChar).Value = Amount;
                cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = project_id;

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //CREATE SAMPLE FUNCTION
        public void Create_Sample_calculate()
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                //string[] stringarr = new string[11] { "Concrete Amount", "Reinforcement Amount", "Plaster Amount", "Paint Amount", "Tiling Amount", "Formwork Amount", "Waterproofing Amount", "Door window Amount", "Sanitary fitting Amount", "Pantry Amount", "Plumbing Amount" };


                //for (int i = 0; i < stringarr.Length; i++)
                //{
                  
               

                Int32 count;
                SqlCommand com = new SqlCommand("SELECT Top 1 id FROM quantity_data Order by id desc ", con);
                count = Convert.ToInt32(com.ExecuteScalar());
                if (count < 0)
                {
                    count = 1;
                }
                else
                {
                    count += 1;
                }

                cmd.CommandText = "INSERT INTO quantity_data(id, Type,Unit,Quantity,Rate,Amount,project_id) VALUES(@id, @Type, @Unit, @Quantity, @Rate, @Amount, @project_id ) ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = count;
                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = Type;
                cmd.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Unit;
                cmd.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = Quantity;
                cmd.Parameters.Add("@Rate", SqlDbType.VarChar).Value = Rate;
                cmd.Parameters.Add("@Amount", SqlDbType.VarChar).Value = Amount;
                cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = count;

                cmd.ExecuteNonQuery();
                con.Close();
                //}
              
            }
        }

        //READ FUNCTION
        public void Read_calculate(int ID)
        {
            dt.Clear();
            SqlCommand cmd = new SqlCommand();
           // DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            con.Open();

            cmd.CommandText = "spGetQuantityData";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters["@ID"].Value = ID;

            cmd.Connection = con;
           // ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds, "spGetQuantityData");

            dt = ds.Tables[0];
            con.Close();

        }

        //UPDATE FUNCTION
        public void Update_calculate()
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "UPDATE quantity_data SET Type=@Type, Unit=@Unit, Quantity=@Quantity, Rate=@Rate, Amount=@Amount  WHERE id=@id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = Type;
                cmd.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Unit;
                cmd.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = Quantity;
                cmd.Parameters.Add("@Rate", SqlDbType.VarChar).Value = Rate;
                cmd.Parameters.Add("@Amount", SqlDbType.VarChar).Value = Amount;

                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        //DELETE FUNCTION
        public void Delete_calculate()
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                Int32 count;
                SqlCommand com = new SqlCommand("SELECT Top 1 id FROM quantity_data Order by id desc ", con);
                count = Convert.ToInt32(com.ExecuteScalar());
                if (count < 0)
                {
                    count = 1;
                }
                else
                {
                    count += 1;
                }

                cmd.CommandText = "DELETE FROM quantity_data WHERE id=@id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = count;

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //CONCRETE AMOUNT
        public void Get_ConcreteAmountType(int project_id)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "SELECT SUM(Quantity) AS 'Total Concrete Amount' FROM quantity_data WHERE Type=@Type and project_id=@project_id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = ConcreteAmountType;
                cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = project_id;

                ConcreteAmount = Convert.ToInt64(cmd.ExecuteScalar());
                con.Close();
            }

        }

        //ReinforcementAmount
        public void Get_ReinforcementAmountType(int project_id)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "SELECT SUM(Quantity) AS 'Total Reinforcement Amount' FROM quantity_data WHERE Type=@Type and project_id=@project_id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = ReinforcementAmountType;
                cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = project_id;

                ReinforcementAmount = Convert.ToInt64(cmd.ExecuteScalar());
                con.Close();
            }

        }

        //PlasterAmount
        public void Get_PlasterAmountType(int project_id)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "SELECT SUM(Quantity) AS 'Total Plaster Amount' FROM quantity_data WHERE Type=@Type and project_id=@project_id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = PlasterAmountType;
                cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = project_id;

                PlasterAmount = Convert.ToInt64(cmd.ExecuteScalar());
                con.Close();
            }

        }

        //PaintAmount
        public void Get_PaintAmountType(int project_id)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "SELECT SUM(Quantity) AS 'Total Paint Amount' FROM quantity_data WHERE Type=@Type and project_id=@project_id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = PaintAmountType;
                cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = project_id;

                PaintAmount = Convert.ToInt64(cmd.ExecuteScalar());
                con.Close();
            }

        }

        //TilingAmount
        public void Get_TilingAmountType(int project_id)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "SELECT SUM(Quantity) AS 'Total Tiling Amount' FROM quantity_data WHERE Type=@Type and project_id=@project_id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = TilingAmountType;
                cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = project_id;

                TilingAmount = Convert.ToInt64(cmd.ExecuteScalar());
                con.Close();
            }

        }

        //FormworkAmount
        public void Get_FormworkAmountType(int project_id)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "SELECT SUM(Quantity) AS 'Total Formwork Amount' FROM quantity_data WHERE Type=@Type and project_id=@project_id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = FormworkAmountType;
                cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = project_id;

                FormworkAmount = Convert.ToInt64(cmd.ExecuteScalar());
                con.Close();
            }

        }

        //WaterproofingAmount
        public void Get_WaterproofingAmountType(int project_id)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "SELECT SUM(Quantity) AS 'Total Waterproofing Amount' FROM quantity_data WHERE Type=@Type and project_id=@project_id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = WaterproofingAmountType;
                cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = project_id;

                WaterproofingAmount = Convert.ToInt64(cmd.ExecuteScalar());
                con.Close();
            }

        }

        //DoorwindowAmount
        public void Get_DoorwindowAmountType(int project_id)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "SELECT SUM(Quantity) AS 'Total Doorwindow Amount' FROM quantity_data WHERE Type=@Type and project_id=@project_id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = DoorwindowAmountType;
                cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = project_id;

                DoorwindowAmount = Convert.ToInt64(cmd.ExecuteScalar());
                con.Close();
            }

        }

        //SanitaryfittingAmount
        public void Get_SanitaryfittingAmountType(int project_id)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "SELECT SUM(Quantity) AS 'Total Sanitaryfitting Amount' FROM quantity_data WHERE Type=@Type and project_id=@project_id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = SanitaryfittingAmountType;
                cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = project_id;

                SanitaryfittingAmount = Convert.ToInt64(cmd.ExecuteScalar());
                con.Close();
            }

        }

        //PantryAmount
        public void Get_PantryAmountType(int project_id)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "SELECT SUM(Quantity) AS 'Total Pantry Amount' FROM quantity_data WHERE Type=@Type and project_id=@project_id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = PantryAmountType;
                cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = project_id;

                PantryAmount = Convert.ToInt64(cmd.ExecuteScalar());
                con.Close();
            }

        }

        //PlumbingAmount
        public void Get_PlumbingAmountType(int project_id)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "SELECT SUM(Quantity) AS 'Total Plumbing Amount' FROM quantity_data WHERE Type=@Type and project_id=@project_id ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = PlumbingAmountType;
                cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = project_id;

                PlumbingAmount = Convert.ToInt64(cmd.ExecuteScalar());
                con.Close();
            }

        }

        //Get Project ID
        public void Get_ProjectID()
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())

            {
                cmd.CommandText = "SELECT project_id FROM final_view";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                project_id = cmd.ExecuteNonQuery();
                con.Close();
            }

        }
    }
}
