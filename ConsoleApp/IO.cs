using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
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

        public static void DisplayAccountList(Customer client, List<Account> AccountsList)
        {
            string array = String.Format("The client list is : {0}\n", client.Name);
            foreach (Account account in AccountsList)
            { 
                array += String.Format("{0,-30} | {1,20} | {2,20} | {3, 10:dd/mm/yyyy}\n", account.GetType().Name, account.AccountNumber, account.Amount, account.CreationDate);
            }
            Console.WriteLine($"\n{array}");
        }
    }
}
