using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Project2
{
    class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"DESKTOP-O7HRM7U\SQLEXPRESS";

            string database = "Project2-Banque";
            string username = "project2";
            string password = "azerty";

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }

}