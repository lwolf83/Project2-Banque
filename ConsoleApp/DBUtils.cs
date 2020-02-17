using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Project2
{
    class DBUtils
    {
        public static void
          GetDBConnection()
        {
            ConnectionStringSettings connString = ConfigurationManager.ConnectionStrings["SqlConnection"];
            string connectionString = connString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            IO.DisplayInformation("Getting Connection ...");
            Program.sqlConnexion = conn;

            try
            {
                IO.DisplayInformation("Openning Connection ...");
                conn.Open();
                IO.DisplayInformation("Connection successful!");
            }
            catch (Exception e)
            {
                IO.DisplayWarning("Error: " + e.Message);
            }

        }

    }

}