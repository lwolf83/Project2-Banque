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
            //
            //Data Source=Data Source=DESKTOP-O7HRM7U\SQLEXPRESS;Initial Catalog=Project2-Banque;Integrated Security=True
            //
            //string connString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=Project2-Banque;Integrated Security=True";
            // string connString = @"Data Source=" + datasource + ";Initial Catalog="
            //           + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;
            ConnectionStringSettings connString = ConfigurationManager.ConnectionStrings["Localhostconnection"];
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