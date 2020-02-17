﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Project2
{
    class DBQuery
    {
        public DBQuery()
        {
            
        }

        ~DBQuery()
        {

        }


        public static Customer GetCustomerFromDB(string field, string value)
        {
            string sql = "SELECT idCustomer,login,name,password, location FROM Customer WHERE [" + field + "] = '" + value + "'";

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = Program.sqlConnexion;
            cmd.CommandText = sql;

            Customer currentClient = new Customer();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        currentClient.IdCustomer = reader.GetInt32(reader.GetOrdinal("idCustomer"));
                        currentClient.Login = reader.GetString(reader.GetOrdinal("login"));
                        currentClient.Name = reader.GetString(reader.GetOrdinal("name"));
                        currentClient.Password = reader.GetString(reader.GetOrdinal("password"));
                        currentClient.Location = reader.GetString(reader.GetOrdinal("location"));
                    }
                }
            }
            
            return currentClient;
        }

        public static Customer getCustomerFromDbWhereLogin(string login)
        {
            return GetCustomerFromDB("login", login);
        }

        public static Customer getCustomerFromDbWhereID(int id)
        {
            string idClient = Convert.ToString(id);
            return GetCustomerFromDB("idClient", idClient);
        }

        public static void SaveNewCustomerInDb(string name, string login, string password, string location)
        {
            string sql = "INSERT INTO Customer (name,login,password,location) "
                    + " VALUES ('" + name + "', '" + login + "' , '" + password + "' , '" + location + "')";
            ExecuteQuery(sql);

        }

        public static void SaveNewAccountInDb(Account account)
        {
            string typeOfAccount;
            Type type = account.GetType();
            if (type.Name == "CheckingAccount")
            {
                typeOfAccount = "CA";
            }
            else
            {
                typeOfAccount = "SA";
            }

            string sql = "INSERT INTO Account (idCustomer,accountNumber,amount,type) "
                        + " VALUES ('" + account.IdCustomer + "','" + account.AccountNumber + "','" + account.Amount + "','" + typeOfAccount + "')";

            ExecuteQuery(sql);

        }

        public static int GetIdCustomerFromAccountNumber(string accountNumber)
        {
            string sql = "SELECT [idCustomer] FROM Account WHERE accountNumber = '"+ accountNumber +"'";

            // Créez un objet Command.
            SqlCommand cmd = new SqlCommand();

            // Combinez l'objet Command avec Connection.
            cmd.Connection = Program.sqlConnexion;
            cmd.CommandText = sql;
            int IdCustomer = 0;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    IdCustomer = reader.GetInt32(reader.GetOrdinal("idCustomer"));
                }
            }
            return IdCustomer;
        }

        public static Account GetAccountFromDB(string accountNumber)
        {
            string sql = "SELECT idCustomer,idAccount,amount, type, isDebitAuthorized, creationDate " +
                            "FROM [Account] WHERE accountNumber = '" + accountNumber + "'";


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Program.sqlConnexion;
            cmd.CommandText = sql;

            int idCustomer = 0;
            int idAccount = 0;
            decimal amount = 0;
            string type = "";
            bool isDebitAuthorized = true;
            DateTime creationDate = new DateTime();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    idCustomer = reader.GetInt32(reader.GetOrdinal("idCustomer"));
                    idAccount = reader.GetInt32(reader.GetOrdinal("idAccount"));
                    amount = reader.GetDecimal(reader.GetOrdinal("amount"));
                    type = reader.GetString(reader.GetOrdinal("type"));
                    isDebitAuthorized = reader.GetBoolean(reader.GetOrdinal("isDebitAuthorized"));
                    creationDate = reader.GetDateTime(reader.GetOrdinal("creationDate"));
                }
            }
            Account resultAccount;
            if(type == "CA")
            {
                resultAccount = new CheckingAccount();
            }
            else
            {
                resultAccount = new SavingsAccount();
            }
            resultAccount.AccountNumber = accountNumber;
            resultAccount.Amount = amount;
            resultAccount.CreationDate = creationDate;
            resultAccount.IdAccount = idAccount;
            resultAccount.IdCustomer = idCustomer;
            resultAccount.IsDebitAuthorized = isDebitAuthorized;
            return resultAccount;
        }

        public static bool IsCurrentCustomerAuthorizedOnAccount(Account account)
        {
            string sql = "SELECT COUNT([idCustomer]) AS nbCustomer FROM AccountAuthorizedCustomers WHERE idAccount = '" + account.IdAccount 
                + "' AND idCustomer = '" + Program.currentCustomer.IdCustomer + "'";

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = Program.sqlConnexion;
            cmd.CommandText = sql;
            int nbCustomer = 0;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    nbCustomer = reader.GetInt32(reader.GetOrdinal("nbCustomer"));
                }
            }

            if(nbCustomer == 0)
            {
                return false;
            }
            return true;
        }

        public static void  UpdateAmountInAccount(Account account)
        {
            string sql = "UPDATE Account SET amount = '" + account.Amount + "' WHERE idAccount = " + "'" + account.IdAccount + "'";

            ExecuteQuery(sql);
        }

        public static void UpdateAccountNumberInAccount(Account account)
        {
            string sql = "UPDATE Account SET accountNumber = '" + account.AccountNumber + "' WHERE idAccount = " + "'" + account.IdAccount + "'";

            ExecuteQuery(sql);
        }

        public static void InsertTransaction(Transaction currentTransaction)
        {
            string transactionType="";
            string startDateString="null";
            string endDateString="null";
            int Periodicity=0;
            string TransferDate = "";



            if (currentTransaction.GetType().Name == "Instant")
            {
                transactionType = "Instant";
                startDateString = "null";
                endDateString = "null";
                TransferDate = "'" + Convert.ToString(currentTransaction.TransferDate) + "'";
            }
            else if (currentTransaction.GetType().Name == "Deferred")
            {
                transactionType = "Deferred";
                startDateString = "null";
                endDateString = "null";
                TransferDate = "'" + Convert.ToString(currentTransaction.TransferDate) + "'";
            }
            else if(currentTransaction.GetType().Name == "Permanent")
            {
                transactionType = "Permanent";
                Permanent permanentTransaction = (Permanent) currentTransaction;
                startDateString = "'" + permanentTransaction.StartDate.ToString("yyyy-MM-dd") + "'";
                endDateString = "'" + permanentTransaction.EndDate.ToString("yyyy-MM-dd") + "'";
                Periodicity = permanentTransaction.Periodicity;
                TransferDate = "null"; 
            }

            string sql = "INSERT INTO[Transaction] (idOriginAccount, idDestinationAccount, amount, transactionType, transactionDate, transferDate, beginDate, endDate," +
                "periodicity) VALUES('" + currentTransaction.AccountOrigin + "' , '" + currentTransaction.AccountDestination + "', '" +
                currentTransaction.Amount + "','"+ transactionType + "', GetDate()," + TransferDate + "," + startDateString + "," + endDateString + ",'" + Periodicity + "'); ";

            ExecuteQuery(sql);
        }

        public static List<Account> GetAccountsCustomer(int idCustomer)
        {
            string sql = " SELECT[idAccount],[idCustomer],[accountNumber],[amount],[type],[isDebitAuthorized],[creationDate] FROM Account " +
                "WHERE idCustomer = " + idCustomer;

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = Program.sqlConnexion;
            cmd.CommandText = sql;

            List<Account> ListAccountsCustomer = new List<Account>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        string typeAccount = reader.GetString(reader.GetOrdinal("type")); ;
                        Account currentCustomerAccount;
                        if (typeAccount == "CA")
                        {
                            currentCustomerAccount = new CheckingAccount();
                        }
                        else
                        {
                            currentCustomerAccount = new SavingsAccount();
                        }

                        currentCustomerAccount.IdAccount = reader.GetInt32(reader.GetOrdinal("idAccount"));
                        currentCustomerAccount.IdCustomer = reader.GetInt32(reader.GetOrdinal("idCustomer"));
                        currentCustomerAccount.AccountNumber = reader.GetString(reader.GetOrdinal("accountNumber"));
                        currentCustomerAccount.Amount = reader.GetDecimal(reader.GetOrdinal("amount"));
                        currentCustomerAccount.IsDebitAuthorized = reader.GetBoolean(reader.GetOrdinal("isDebitAuthorized"));
                        currentCustomerAccount.CreationDate = reader.GetDateTime(reader.GetOrdinal("creationDate"));
                        ListAccountsCustomer.Add(currentCustomerAccount);
                    }
                }
                return ListAccountsCustomer;
            }
        }

        public static List<Transaction> GetTransactionFromDB(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Program.sqlConnexion;
            cmd.CommandText = sql;

            List<Transaction> currentTransactionList = new List<Transaction>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Transaction newTransaction;
                        string TransactionType = reader.GetString(reader.GetOrdinal("transactionType"));
                        if (TransactionType == "Instant")
                        {
                            newTransaction = new Instant();
                        }
                        else if (TransactionType == "Deffered")
                        {
                            newTransaction = new Deferred();
                        }
                        else
                        {
                            newTransaction = new Permanent();
                            Permanent newPermanentTransaction = (Permanent)newTransaction;

                            newPermanentTransaction.StartDate = reader.GetDateTime(reader.GetOrdinal("beginDate"));
                            newPermanentTransaction.EndDate = reader.GetDateTime(reader.GetOrdinal("endDate"));
                            newPermanentTransaction.Periodicity = reader.GetInt32(reader.GetOrdinal("periodicity"));
                        }
                        newTransaction.IdTransaction = reader.GetInt32(reader.GetOrdinal("idTransaction"));
                        newTransaction.TransactionDate = reader.GetDateTime(reader.GetOrdinal("transactionDate"));
                        newTransaction.TransferDate = reader.GetDateTime(reader.GetOrdinal("transferDate"));
                        newTransaction.Amount = reader.GetDecimal(reader.GetOrdinal("amount"));
                        newTransaction.AccountOrigin = reader.GetInt32(reader.GetOrdinal("idOriginAccount"));
                        newTransaction.AccountDestination = reader.GetInt32(reader.GetOrdinal("idDestinationAccount"));

                        currentTransactionList.Add(newTransaction);
                    }
                }
            }
            return currentTransactionList;
        }

        public static List<Transaction> GetTransactionFromIdAccount(Account currentIdAccount)
        {
            string sql = "SELECT [transaction].idTransaction, [transaction].idOriginAccount, [transaction].idDestinationAccount, [transaction].amount, [transaction].transactionDate, [transaction].transferDate, [transaction].beginDate, [transaction].endDate, [transaction].periodicity FROM [Transaction] INNER JOIN Account ON[Transaction].idOriginAccount = Account.idAccount WHERE Account.idAccount = " + currentIdAccount + ";";
            return GetTransactionFromDB(sql);
        }

        public static List<Transaction> GetTransactionFromLogin(Customer currentLogin)
        {
            string sql = "SELECT [transaction].idTransaction, [transaction].idOriginAccount, [transaction].idDestinationAccount, [transaction].amount, [transaction].transactionDate, [transaction].transferDate, [transaction].beginDate, [transaction].endDate, [transaction].periodicity FROM [Transaction] INNER JOIN Account ON[Transaction].idOriginAccount = Account.idAccount INNER JOIN Customer ON Account.idCustomer = Customer.idCustomer WHERE Customer.login = '" + currentLogin + "'";
            return GetTransactionFromDB(sql);
        }

        public static List<Transaction> GetTransactionBetweenTwoDates(DateTime Date1, DateTime Date2)
        {
            string sql = "SELECT [transaction].idTransaction, [transaction].idOriginAccount, [transaction].idDestinationAccount, [transaction].amount, [transaction].transactionDate, [transaction].transferDate, [transaction].beginDate, [transaction].endDate, [transaction].periodicityFROM [Transaction] WHERE " + Date1 + " > transferDate AND transferDate > " + Date2 + "; ";
            return GetTransactionFromDB(sql);
        }

        private static void ExecuteQuery(string sql)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = Program.sqlConnexion;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
    }
}
