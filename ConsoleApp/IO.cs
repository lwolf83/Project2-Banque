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

        public static void TextCenter(string text)
        {
            int spaceNb = (Console.WindowWidth - text.Length) / 2;
            Console.SetCursorPosition(spaceNb, Console.CursorTop);
            Console.WriteLine(text);
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
