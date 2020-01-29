using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Project2
{
    class DBSQLServerUtils
    {

        public static SqlConnection
                 GetDBConnection(string datasource, string database, string username, string password)
        {
            //
            //Data Source=Data Source=DESKTOP-O7HRM7U\SQLEXPRESS;Initial Catalog=Project2-Banque;Integrated Security=True
            //
            string connString = @"Data Source=DESKTOP-O7HRM7U\SQLEXPRESS;Initial Catalog=Project2-Banque;Integrated Security=True";
           // string connString = @"Data Source=" + datasource + ";Initial Catalog="
           //           + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }


    }
}