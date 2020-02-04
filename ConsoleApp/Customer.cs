﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class Customer
    {
        public int IdClient { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public List<Account> Accounts { get; set; }

        public Customer()
        { }

        public Customer(string name, string login, string location, string password)
        { }

        public Customer(string login)
        {
            Login = login;
        }

        public bool IsAuthorizedClient()
        {
            return true;
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
            string specialCharacter = "/#+=-*@%&_.;,!?()[]{}<>";
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
            Customer existingCustomer = DBQuery.getCustomerFromDbWhereLogin(login);
            if (existingCustomer.Login == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public void AddSavingAccount()
        {
            SavingsAccount account = new SavingsAccount();
            Accounts.Add(account);
        }

        public void AddCheckingAccount()
        {
            CheckingAccount account = new CheckingAccount();
            Accounts.Add(account);
        }

        public List<Account> GetAccountList()
        {
            CheckingAccount newca1 = new CheckingAccount("CANUM01", 2850.50, 006984);
            CheckingAccount newca2 = new CheckingAccount("CANUM02", 00.00, 006984);
            SavingsAccount newsa1 = new SavingsAccount("SANUM01", 20800.00, 006984);

            List<Account> currentClientAccountsList = new List<Account>();
            currentClientAccountsList.Add(newca1);
            currentClientAccountsList.Add(newca2);
            currentClientAccountsList.Add(newsa1);

            return currentClientAccountsList;
        }
    }
}
