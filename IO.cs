using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class IO
    {
        public static string getAction()
        {
           if (Program.opts.CreateNewClient != "")
            {
                return "CreateClient";
                
            }
           else if (Program.opts.CreateNewAccount!= "")
            {
                if (Program.opts.CreateNewAccount == "sa")
                {
                    return "CreateSavingAccount";
                }
                else if (Program.opts.CreateNewAccount == "ca")
                {
                    return "CreateCheckingAccount";
                }
                else
                {
                    IO.DisplayWarning("Account type doesn't exist");
                    return "Error" ;
                }
            }
            return "";
            

        }

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
