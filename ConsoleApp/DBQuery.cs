﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Project2
{
    class DBQuery
    {
        public static Customer getCustomerFromDB(string field, string value)
        {
            string sql = "SELECT * FROM [dbo].[Customer] WHERE [" + field + "] = '" + value + "'";

            // Créez un objet Command.
            SqlCommand cmd = new SqlCommand();

            // Combinez l'objet Command avec Connection.
            cmd.Connection = Program.sqlConnexion;
            cmd.CommandText = sql;

            
            Customer currentClient = new Customer();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        currentClient.IdClient = reader.GetInt32(reader.GetOrdinal("idCustomer"));
                        currentClient.Login = reader.GetString(reader.GetOrdinal("name"));
                        currentClient.Name = reader.GetString(reader.GetOrdinal("login"));
                        currentClient.Password = reader.GetString(reader.GetOrdinal("password"));
                        currentClient.Location = reader.GetString(reader.GetOrdinal("location"));

                    }
                }
            }
            
            return currentClient;
        }



        public static Customer getCustomerFromDbWhereLogin(string login)
        {
            return getCustomerFromDB("login", login);
        }

        public static Customer getCustomerFromDbWhereID(int id)
        {
            string idClient = Convert.ToString(id);
            return getCustomerFromDB("idClient", idClient);
        }

        public static void saveNewCustomerInDb(int idCustomer,string name, string login, string password, string location, string dateCreation)
        {
            string sql = "INSERT INTO [dbo].[Customer] ([idCustomer],[name],[login],[password],[location]) "
                    + " VALUES ("+ idCustomer + " , '" +name + "', '" + login + "' , '"+ password + "' , '" + location + "')";

            // Créez un objet Command.
            SqlCommand cmd = new SqlCommand();

            // Combinez l'objet Command avec Connection.
            cmd.Connection = Program.sqlConnexion;
            cmd.CommandText = sql;
            DbDataReader reader = cmd.ExecuteReader();
            IO.DisplayInformation("SAuvegarde ok");

        }
    }
}
