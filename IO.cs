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
                
                
            }
   
        }

        public static void SaveDB(Client client)
        {
            IO.DisplayInformation("Client saved in DataBase", Program.verbose);
        }

        public static void DisplayWarning(string message, bool verbose)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            DisplayInformation(message, verbose);
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
