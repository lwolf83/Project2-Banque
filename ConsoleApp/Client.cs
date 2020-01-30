using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class Client
    {
        public int IdClient { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public List<Account> Accounts { get; set; }

        public Client()
        { }

        public Client(string name, string login, string location, string password)
        { }

        public Client(string login)
        {
            Login = login;
        }

        public bool IsLoginExist(string login)
        {
            return true;
        }

        public bool IsAuthorizedClient()
        {
            return true;
        }

        public bool IsClientExisting()
        { // vérifie dans la base de données si le client existe en fonction de son login
            return false;    
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
            foreach(char letter in password)
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
            if(password.Length >= 8)
            {
                return true;
            }
            return false;
        }

        public static bool HasNumber(string password)
        {
            foreach(char character in password)
            {
                if(char.IsDigit(character))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasSpecialCharacter(string password)
        {
            string specialCharacter = "/#+=-*@%&_.;,";
            foreach(char character in password)
            {
                if(specialCharacter.Contains(character))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsCustomerExisting(string login)
        { // vérifie dans la base de données si le client existe en fonction de son login
          // POUR TEST SANS BDD Client existingClient = new Client("jeanbarth");

            Client existingCustomer = DBQuery.getCustomerFromDbWhereLogin(login);
            if (existingCustomer.Login == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool PasswordDifferentFromPasswordInDB(string login, string password)
        {
            Client currentCustomer = DBQuery.getCustomerFromDbWhereLogin(login);

            string passwordInDB = currentCustomer.Password;
            int i = 0;
            while ((password != passwordInDB) && (i < 2))
            {
                Console.WriteLine("Wrong password, please try again");
                password = Console.ReadLine();
                i++;
            }
            if ((i == 2) && (password != passwordInDB))
            {
                Console.WriteLine("You entered 3 times a wrong password, try again in 10 minutes");

            }
            return true;
        }
    }
}