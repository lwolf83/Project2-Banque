using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Project2
{
    public class Customer
    {
        public int IdCustomer { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();


        public Customer(DbDataReader reader)
        {
            IdCustomer = reader.GetInt32(reader.GetOrdinal("idCustomer"));
            Login = reader.GetString(reader.GetOrdinal("login"));
            Name = reader.GetString(reader.GetOrdinal("name"));
            Password = reader.GetString(reader.GetOrdinal("password"));
            Location = reader.GetString(reader.GetOrdinal("location"));
        }


        public Customer(string login)
        {
            Login = login;
        }

        public bool IsAuthorizedClient()
        {
            return true;
        }

        public bool IsAccountOwner(string accountNumber)
        {
            int id = DBQuery.GetIdCustomerFromAccountNumber(accountNumber);
            if (IdCustomer == id)
            {
                return true;
            }
            else
            { 
            IO.DisplayWarning("The origin account is not one of yours, you are not allowed to request a transfer from somebody else's account.");
            return false;
            }
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
            if (existingCustomer == null)
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
            account.IdCustomer = Program.currentCustomer.IdCustomer;
            account.Amount = 0;
            account.AccountNumber = "";

            DBQuery.SaveNewAccountInDb(account);
            Accounts.Add(account);
            
        }

        public void AddCheckingAccount()
        {
            CheckingAccount account = new CheckingAccount();
            account.IdCustomer = Program.currentCustomer.IdCustomer;
            account.Amount = 0;
            account.AccountNumber = "";

            DBQuery.SaveNewAccountInDb(account);
            Accounts.Add(account);
        }

        public void MakeNewInstantTransaction(decimal amount, Account accountOrigin, Account accountDestination)
        {
            
            accountOrigin.Amount = accountOrigin.Amount - amount;

            accountDestination.Amount = accountDestination.Amount + amount;

            Instant currentTransaction = new Instant();
            currentTransaction.AccountOrigin = accountOrigin.IdAccount;
            currentTransaction.AccountDestination = accountDestination.IdAccount;
            currentTransaction.Amount = amount;
            currentTransaction.TransferDate = DateTime.Now;
           // DBQuery.InsertTransaction(currentTransaction);
            //DBQuery.UpdateAmountInAccount(accountOrigin);
            //DBQuery.UpdateAmountInAccount(accountDestination);
            List<TransfertMoney> transfertList = currentTransaction.GetTransferts();
            Console.WriteLine("We do the transfer");
        }


        public void MakeNewPermanentTransaction(decimal amount, Account accountOrigin, Account accountDestination, DateTime startDate, DateTime endDate, int periodicity)
        {


            Permanent currentTransaction = new Permanent();
            currentTransaction.AccountOrigin = accountOrigin.IdAccount;
            currentTransaction.AccountDestination = accountDestination.IdAccount;
            currentTransaction.Amount = amount;
            currentTransaction.TransactionDate = DateTime.Now;
            currentTransaction.StartDate = startDate;
            currentTransaction.EndDate = endDate;
            currentTransaction.Periodicity = periodicity;
            DBQuery.InsertTransaction(currentTransaction);
        }

        public void MakeNewDefferedTransaction(decimal amount, Account accountOrigin, Account accountDestination, DateTime defferedtransactionDate)
        {

            accountOrigin.Amount = accountOrigin.Amount - amount;

            accountDestination.Amount = accountDestination.Amount + amount;

            Deferred currentTransaction = new Deferred();
            currentTransaction.AccountOrigin = accountOrigin.IdAccount;
            currentTransaction.AccountDestination = accountDestination.IdAccount;
            currentTransaction.Amount = amount;
            currentTransaction.TransactionDate = defferedtransactionDate;
            DBQuery.InsertTransaction(currentTransaction);

            DBQuery.UpdateAmountInAccount(accountOrigin);
            DBQuery.UpdateAmountInAccount(accountDestination);


            Console.WriteLine("We do the transfer");
        }


        /*public List<Account> GetAccountList()
        {
            CheckingAccount newca1 = new CheckingAccount("CANUM01", 2850.50, 006984);
            CheckingAccount newca2 = new CheckingAccount("CANUM02", 00.00, 006984);
            SavingsAccount newsa1 = new SavingsAccount("SANUM01", 20800.00, 006984);

            List<Account> currentClientAccountsList = new List<Account>();
            currentClientAccountsList.Add(newca1);
            currentClientAccountsList.Add(newca2);
            currentClientAccountsList.Add(newsa1);

            return currentClientAccountsList;
        }*/
    }
}
