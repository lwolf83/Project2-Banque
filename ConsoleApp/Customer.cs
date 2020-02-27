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
        public List<AbstractAccount> Accounts { get; set; } = new List<AbstractAccount>();


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

        public static bool IsAccountOwner(int customerId, string accountNumber)
        {
            int id = DBQuery.GetIdCustomerFromAccountNumber(accountNumber);
            if (customerId == id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

      
        public static bool IsCustomerExisting(string login)
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
            account.Ceiling = 61200;
            account.SavingsRate = 0.01m;

            DBQuery.SaveNewAccountInDb(account);
            Accounts.Add(account);
            Console.WriteLine("Your Saving Account has been created with default " +
                "values (Ceiling set to - " + account.Ceiling + " and SavingsRate set to  " + account.SavingsRate  + ")");
        }

        public void AddCheckingAccount()
        {
            CheckingAccount account = new CheckingAccount();
            account.IdCustomer = Program.currentCustomer.IdCustomer;
            account.Amount = 0;
            account.AccountNumber = "";
            account.Overdraft = -200;

            DBQuery.SaveNewAccountInDb(account);
            Accounts.Add(account);
            Console.WriteLine("Your Checking Account has been created with default value (Overdraft set to - " + account.Overdraft + ")");
        }

        public void MakeNewTransaction(decimal amount, AbstractAccount accountOrigin, AbstractAccount accountDestination, DateTime? startDate = null, DateTime? endDate = null, int periodicity = 0)
        {
            AbstractTransaction currentTransaction = AbstractTransaction.Create(startDate, endDate);
            currentTransaction.AccountOrigin = accountOrigin.IdAccount;
            currentTransaction.AccountDestination = accountDestination.IdAccount;
            currentTransaction.Amount = amount;
            currentTransaction.Periodicity = periodicity;
            currentTransaction.TransactionDate = DateTime.Now;
            DBQuery.InsertTransaction(currentTransaction);
            List<TransferMoney> transfertList = currentTransaction.GetTransferts();
            Console.WriteLine("We do the transfer");
            DBQuery.SaveNewTransferInDb(transfertList);
        }

        public static List<AbstractAccount> GetAccountList(int id)
        {          
            List<AbstractAccount> currentCustomerAccountsList = DBQuery.GetAccountsCustomer(id);

            return currentCustomerAccountsList;
        }
    }
}
