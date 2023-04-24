using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FINAL_PROJECT_NIBM.App_Code.Data
{
    class DBUtils
    {
        public SqlConnection con;
        public DBUtils()
        {
       
            string constring = "Data Source=DESKTOP-ITEQDHG;Initial Catalog=QSestimateDB;Integrated Security=True";

            con = new SqlConnection(constring);
        }

       public string constring = "Data Source=DESKTOP-ITEQDHG;Initial Catalog=QSestimateDB;Integrated Security=True";

    }
}
