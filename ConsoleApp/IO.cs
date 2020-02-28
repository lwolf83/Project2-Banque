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

        public static void SaveDB(AbstractAccount account)
        {
            IO.DisplayInformation("Account saved in DataBase");
        }

        public static void DisplayWarning(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
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

        public static void DisplayAccountList(Customer customer, List<AbstractAccount> AccountsList)
        {
            string array = String.Format("The client list is : {0}\n", customer.Name);
            string lignTemplate = "|{0, -29} | {1,20} | {2,20} | {3, 10:dd/MM/yyyy}|\n";
            string header = String.Format(lignTemplate, "Type Account", "Account Number", "Amount", "Date");
            string separationLine = new String('-', header.Length) +"\n";
            array += separationLine + header + separationLine;
       
            foreach (AbstractAccount account in AccountsList)
            { 
                array += String.Format("|{0,-29} | {1,20} | {2,20} | {3, 10:dd/MM/yyyy}|\n", account.GetType().Name, account.AccountNumber, account.Amount, account.CreationDate);
            }
            array += separationLine;
            Console.WriteLine($"\n{array}");
            //ajouter un message d'erreur si compte n'existe pas
        }

        public static void DisplayTransactionList(List<AbstractTransaction> TransactionList)
        {
            string array = String.Format("The transaction list of {0}\n", Program.currentCustomer.Name);
            string lignTemplate = "|{0, 10:dd/MM/yyyy} | {1,20} | {2,20} | {3, 20}|\n";
            string header = String.Format(lignTemplate, "Date", "Amount", "Account Origin", "Account Destination");
            string separationLine = new String('-', header.Length) + "\n";
            array += separationLine + header + separationLine;
            foreach (AbstractTransaction transaction in TransactionList)
            {
                array += String.Format(lignTemplate, transaction.TransactionDate, transaction.Amount, transaction.AccountOrigin, transaction.AccountDestination);
            }
            array += separationLine;
            Console.WriteLine($"\n{array}");
        }

        public static string PromptPassword()
        {
            string password = "";
            bool finished = false;
            while (finished == false)
            {
                ConsoleKeyInfo typedCharacterInfo = Console.ReadKey(true);
                if (typedCharacterInfo.Key != ConsoleKey.Enter)
                {
                    password += typedCharacterInfo.KeyChar;
                }
                else
                {
                    finished = true;
                }
            }
            return password;
        }

        public static bool IsComplexPassword(string password)
        {
            bool hasUpperletter = HasUpperletter(password);
            bool hasLowerletter = HasLowerletter(password);
            bool hasEnoughLetters = HasEnoughLetters(password);
            bool hasNumber = HasNumber(password);
            bool hasSpecialCharacter = HasSpecialCharacter(password);
            bool result = hasUpperletter && hasLowerletter && hasEnoughLetters && hasNumber && hasSpecialCharacter;
            return result;
        }

        public static bool HasUpperletter(string password)
        {
            foreach (char letter in password)
            {
                if (char.IsUpper(letter))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasLowerletter(string password)
        {
            foreach (char letter in password)
            {
                if (char.IsLower(letter))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasEnoughLetters(string password)
        {
            if (password.Length >= 8)
            {
                return true;
            }
            return false;
        }

        public static bool HasNumber(string password)
        {
            foreach (char character in password)
            {
                if (char.IsDigit(character))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasSpecialCharacter(string password)
        {
            string specialCharacter = "/#+=-*@%&_.;,!?()[]{}<>²~'|`\\\"^°£$¤µ¨§:";
            foreach (char character in password)
            {
                if (specialCharacter.Contains(character))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
