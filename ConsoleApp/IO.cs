using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Project2
{
    class IO
    {
        public static void SaveDB(Customer client)
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
            if(Program.Verbose)
            {
                Console.WriteLine(message);
            }
        }

        public static Customer getCustomerFromDB(string customer)
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
 
    }
}
