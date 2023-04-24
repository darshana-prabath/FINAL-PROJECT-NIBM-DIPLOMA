using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Odbc;

namespace FINAL_PROJECT_NIBM.App_Code.Data
{
    class DatabaseUtils
    {
        public string DBConnectionStr;
        public int OpenByODBC = 0;


        #region Constructor

        public DatabaseUtils()
        {

            string s = System.Configuration.ConfigurationSettings.AppSettings.Get("sqlcn");
            if ((this.OpenByODBC) == 0)
            {
                //s = "Data Source=DESKTOP-ITEQDHG;Initial Catalog=Student;Integrated Security=True";
                s = "Data Source=DESKTOP-ITEQDHG;Initial Catalog=QSestimateDB;Integrated Security=True";
            }
            else
            {

            }
            this.DBConnectionStr = s;
        }

        #endregion

        #region Open/Close DB Connections

        private SqlConnection OpenConnection()
        {
            SqlConnection cn = new SqlConnection(DBConnectionStr);
            cn.Open();
            return cn;
        }

        private OdbcConnection OpenOdbcConnection()
        {
            OdbcConnection cn = new OdbcConnection(DBConnectionStr);
            cn.Open();
            return cn;
        }

        private void CloseConnection(SqlConnection cn)
        {
            cn.Close();
            cn.Dispose();
        }

        private void CloseConnection(OdbcConnection cn)
        {
            cn.Close();
            cn.Dispose();
        }


        #endregion

        #region  Execute General SQL statemenets

        public DataTable Return(string sQuery)
        {
            try
            {
                if ((this.OpenByODBC) == 1)
                {
                    OdbcConnection ODBCConnection = this.OpenOdbcConnection();
                    OdbcCommand ODBCCommand = ODBCConnection.CreateCommand();
                    ODBCCommand.CommandTimeout = 2147483647;
                    ODBCCommand.CommandText = sQuery;
                    OdbcDataAdapter ODBCDataAdapter = new OdbcDataAdapter();
                    ODBCDataAdapter.SelectCommand = ODBCCommand;
                    DataSet oDS = new DataSet();
                    ODBCDataAdapter.Fill(oDS, "Table1");
                    this.CloseConnection(ODBCConnection);

                    return oDS.Tables[0];
                }
                else
                {
                    SqlConnection mySqlConnection = this.OpenConnection();
                    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                    mySqlCommand.CommandTimeout = 2147483647;
                    mySqlCommand.CommandText = sQuery;
                    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
                    mySqlDataAdapter.SelectCommand = mySqlCommand;
                    DataSet myDataSet = new DataSet();
                    mySqlDataAdapter.Fill(myDataSet, "Table1");
                    this.CloseConnection(mySqlConnection);

                    return myDataSet.Tables[0];
                }
            }
            catch (Exception ex) { throw ex; }
        }

        // Execute SQL Command
        public bool ExecuteCommand(string sCommand)
        {
            int ret = 0;

            if ((this.OpenByODBC) == 1)
            {
                OdbcConnection cn = this.OpenOdbcConnection();
                OdbcCommand cmd = new OdbcCommand(sCommand, cn);
                ret = cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.CloseConnection(cn);
            }
            else
            {

                SqlConnection cn = this.OpenConnection();
                SqlCommand cmd = new SqlCommand(sCommand, cn);
                ret = cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.CloseConnection(cn);
            }

            return (ret > 0);
        }

        // Execute SQL Query and return single value
        public object ExecuteScalar(string sQuery)
        {

            object o = null;

            if ((this.OpenByODBC) == 1)
            {
                OdbcConnection cn = this.OpenOdbcConnection();
                OdbcCommand cmd = new OdbcCommand(sQuery, cn);
                o = cmd.ExecuteScalar();
                cmd.Dispose();
                this.CloseConnection(cn);
            }
            else
            {


                SqlConnection cn = this.OpenConnection();
                SqlCommand cmd = new SqlCommand(sQuery, cn);
                o = cmd.ExecuteScalar();
                cmd.Dispose();
                this.CloseConnection(cn);
            }
            if (o == DBNull.Value)
                return null;
            else
                return o;
        }

        // Execute SQL Query and return data table
        public DataTable ExecuteQuery(string sQuery)
        {

            DataTable dt = null;

            if ((this.OpenByODBC) == 1)
            {
                OdbcConnection cn = this.OpenOdbcConnection();
                OdbcCommand cmd = new OdbcCommand(sQuery, cn);
                cmd.CommandTimeout = 483647;
                cmd.CommandType = CommandType.Text;
                OdbcDataAdapter da = new OdbcDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.Dispose();
                da.Dispose();
                this.CloseConnection(cn);
            }
            else
            {
                SqlConnection cn = this.OpenConnection();
                SqlDataAdapter da = new SqlDataAdapter(sQuery, cn);
                dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                this.CloseConnection(cn);
            }
            return dt;
        }


        #endregion

        #region Insert/Update/Delete

        // Inserts new record, and returns the @@identity value (autonumber) generated for the 'id' field.
        public Int64 Insert(DataTable dt)
        {
            return this.Insert(dt, "*");
        }

        // Inserts new record, and returns the @@identity value (autonumber) generated for the 'id' field.
        public Int64 Insert(DataTable dt, string FieldList)
        {
            if (dt.Rows.Count == 0)
            {
                return -1;
            }

            string sql = "SELECT " + FieldList + " FROM " + dt.TableName + " WHERE 1=0";

            if ((this.OpenByODBC) == 1)
            {
                OdbcConnection cn = this.OpenOdbcConnection();
                OdbcDataAdapter da = new OdbcDataAdapter(sql, cn);
                OdbcCommandBuilder cb = new OdbcCommandBuilder(da);

                dt.Rows[0]["regNo"] = -1;

                da.RowUpdated += new OdbcRowUpdatedEventHandler((OnTableRowUpdated));

                da.Update(dt);
                cb.Dispose();
                da.Dispose();
                this.CloseConnection(cn);
            }
            else
            {

                SqlConnection cn = this.OpenConnection();
                SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);

                dt.Rows[0]["regNo"] = -1;

                da.RowUpdated += new SqlRowUpdatedEventHandler(OnTableRowUpdated);

                da.Update(dt);
                cb.Dispose();
                da.Dispose();
                this.CloseConnection(cn);
            }
            return (Int64)dt.Rows[0]["regNo"];
        }

        // this is a private event handler to get the identity/auto_number generated on a inserted row
        private void OnTableRowUpdated(object sender, SqlRowUpdatedEventArgs args)
        {
            if (args.StatementType == StatementType.Insert)
            {
                // Retrieve the identity value and save it back in the row's ID field
                SqlConnection cn = args.Command.Connection;
                SqlCommand cmd = new SqlCommand("SELECT @@IDENTITY", cn);
                object o = cmd.ExecuteScalar();
                if (!(o is DBNull))
                    args.Row["regNo"] = int.Parse(o.ToString());

                cmd.Dispose();
            }
        }

        private void OnTableRowUpdated(object sender, OdbcRowUpdatedEventArgs args)
        {
            if (args.StatementType == StatementType.Insert)
            {
                // Retrieve the identity value and save it back in the row's ID field
                OdbcConnection cn = args.Command.Connection;
                OdbcCommand cmd = new OdbcCommand("SELECT @@IDENTITY", cn);
                object o = cmd.ExecuteScalar();
                if (!(o is DBNull))
                    args.Row["regNo"] = Int64.Parse(o.ToString());

                cmd.Dispose();
            }
        }

        // Update Database Returns True if Success
        public SqlDataReader ExecuteReader(string text)
        {
            SqlDataReader rd;
            SqlConnection cn = this.OpenConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = text;
            rd = cmd.ExecuteReader();
            cmd.Dispose();
            //this.CloseConnection(cn);
            return rd;
        }

        public OdbcDataReader ExecuteOdbcReader(string text)
        {
            OdbcDataReader rd;
            OdbcConnection cn = this.OpenOdbcConnection();
            OdbcCommand cmd = new OdbcCommand();
            cmd.Connection = cn;
            cmd.CommandText = text;
            rd = cmd.ExecuteReader();
            cmd.Dispose();
            //this.CloseConnection(cn);
            return rd;
        }
        public bool Update(DataTable dt)
        {
            string sql = "SELECT * FROM " + dt.TableName + " WHERE 1=0";

            int ret = 0;

            if ((this.OpenByODBC) == 1)
            {
                OdbcConnection cn = this.OpenOdbcConnection();
                OdbcDataAdapter da = new OdbcDataAdapter(sql, cn);
                OdbcCommandBuilder cb = new OdbcCommandBuilder(da);
                ret = da.Update(dt);
                cb.Dispose();
                da.Dispose();
                this.CloseConnection(cn);
            }
            else
            {


                SqlConnection cn = this.OpenConnection();
                SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                ret = da.Update(dt);
                cb.Dispose();
                da.Dispose();
                this.CloseConnection(cn);
            }
            return (ret > 0);
        }

        // a DataTable will be returned with table metadata so that new rows can be inserted
        public DataTable GetDataTableForInsert(string TableName)
        {
            return this.GetDataTableForInsert(TableName, "*");
        }

        // a DataTable will be returned with table metadata so that new rows can be inserted
        public DataTable GetDataTableForInsert(string TableName, string FieldList)
        {
            string sql = "SELECT " + FieldList + "  FROM " + TableName + " WHERE 1=0";
            DataTable dt = null;


            if ((this.OpenByODBC) == 1)
            {
                OdbcConnection cn = this.OpenOdbcConnection();
                OdbcDataAdapter da = new OdbcDataAdapter(sql, cn);
                dt = new DataTable(TableName);
                da.Fill(dt);
                da.Dispose();
                this.CloseConnection(cn);
            }
            else
            {
                SqlConnection cn = this.OpenConnection();
                SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                dt = new DataTable(TableName);
                da.Fill(dt);
                da.Dispose();
                this.CloseConnection(cn);
            }
            return dt;
        }

        // a DataTable will be returned with table metadata so that the row with 'id' can be updated
        public DataTable GetDataTableForUpdate(string TableName, Int64 id)
        {
            return this.GetDataTableForUpdate(TableName, "*", id);
        }

        // a DataTable will be returned with table metadata so that the row with 'id' can be updated
        public DataTable GetDataTableForUpdate(string TableName, string FieldList, Int64 id)
        {
            string sql = "SELECT " + FieldList + " FROM " + TableName + " WHERE id=" + id;
            return this.GetDataTableForUpdate(TableName, sql);
        }

        // a DataTable will be returned with table metadata so that the row(s) can be updated
        public DataTable GetDataTableForUpdate(string TableName, string SelectCommand)
        {

            DataTable dt = null;

            if ((this.OpenByODBC) == 1)
            {
                OdbcConnection cn = this.OpenOdbcConnection();
                OdbcDataAdapter da = new OdbcDataAdapter(SelectCommand, cn);
                dt = new DataTable(TableName);
                da.Fill(dt);
                da.Dispose();
                this.CloseConnection(cn);
            }
            else
            {


                SqlConnection cn = this.OpenConnection();
                SqlDataAdapter da = new SqlDataAdapter(SelectCommand, cn);
                dt = new DataTable(TableName);
                da.Fill(dt);
                da.Dispose();
                this.CloseConnection(cn);
            }
            return dt;
        }

        // execute a SQL Delete command to delete the row containing the 'id'
        public bool DeleteTableRow(string TableName, Int64 id)
        {
            string sql = "DELETE FROM " + TableName + " WHERE regNo=" + id;
            return this.ExecuteCommand(sql);
        }

        #endregion

        #region Formatting for database field comparrisons when constructing SQL statements

        // returns a string enclosed in ' formatted correctly to be used in a SQL
        // eg:  abc'def will return 'abc''def'
        public string FormatStr(string s)
        {
            return "'" + s.Replace("'", "''") + "'";
        }

        // returns a date formatted correctly to be used in a SQL.
        // we convert the date to SQL date-format-type: 101
        // eg:  Convert(datetime, '06/20/2003', 101)
        public string FormatDate(DateTime d)
        {
            return "Convert(datetime, '" + d.ToString("MM/dd/yyyy") + "', 101)";
        }

        // returns a date-time formatted correctly to be used in a SQL
        // we convert the date to SQL date-format-type: 120
        // eg:  Convert(datetime, '2003-06-20 14:15:16', 120)
        public string FormatDateTime(DateTime d)
        {
            return "Convert(datetime, '" + d.ToString("yyyy-MM-dd hh:mm:ss") + "', 120)";
        }

        #endregion
    }
}

