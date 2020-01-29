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

            //vérifier dans la BDD si le login existe
            Client clientWhoWantsToLogIn = new Client();
            login = Program.opts.Login;

            if (login == clientWhoWantsToLogIn.Login)
            {
                return true;
            }
            return false;
        }

        public bool IsAuthorizedClient()
        {
            return true;
        }

        public bool IsClientExisting()
        { // vérifie dans la base de données si le client existe en fonction de son login
            Client existingClient = new Client("jeanbarth");
            Client clientWhoWantsToLogIn = new Client(Login);
            if (clientWhoWantsToLogIn.Login == existingClient.Login)
            {
                return true;
            }

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
    }
}