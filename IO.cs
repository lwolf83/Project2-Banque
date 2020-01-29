using System;
using System.Collections;
using System.Collections.Generic;
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

        public static void DisplayAccountList(Client client, List<Account> AccountsList)
        {
            Console.WriteLine(Program.currentClient.Name + " account list is below:");
            Console.WriteLine();
            Console.WriteLine("Client ID" + "|" + "Account ID" + "|" + "Amount" + "|" + "Creation Date");
            Console.WriteLine("----------------------------------------------------------------------");

            foreach (Account account in AccountsList)
            {
              Console.WriteLine(account.IdClient + "|" + account.AccountIdentifier + "|" + account.Amount + "|" + account.CreationDate);
            }   
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
