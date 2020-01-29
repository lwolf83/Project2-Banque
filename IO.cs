using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Project2
{
    class IO
    {
        public static void SaveDB(Client client)
        {
            IO.DisplayInformation("Client saved in DataBase");
        }
        public static void SaveDB(Account account)
        {
            IO.DisplayInformation("Account saved in DataBase");
        }
        public static void DisplayWarning(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            DisplayInformation(message);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void DisplayInformation(string message)
        {
            if(Program.opts.Verbose)
            {
                Console.WriteLine(message);
            }
        }

        public static Client getCustomerFromDB(string customer)
        {
            return null;
        }
        /* public static void GetTypeAccount(string[] args)
         {
             string typeAccount;
             //recupere les infos via commandlineparser

             if (typeAccount == "SA")
             {

             }
             else if( typeAccount == "CA" )
             {

             }

         }*/
        private static Client getCustomerFromLogin(string login)
        {
            string sql = "SELECT [idClient] ,[name] ,[login] ,[password] ,[location] FROM [dbo].[User] WHERE [login] = '" + login + "'";

            // Créez un objet Command.
            SqlCommand cmd = new SqlCommand();

            // Combinez l'objet Command avec Connection.
            cmd.Connection = Program.conn;
            cmd.CommandText = sql;

            Client currentClient = new Client();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        int idClient = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string password = reader.GetString(3);
                        string location = reader.GetString(4);

                        currentClient.IdClient = idClient;
                        currentClient.Login = login;
                        currentClient.Name = name;
                        currentClient.Password = password;
                        currentClient.Location = location;
                    }
                }
            }

            return currentClient;

        }
    }
}
