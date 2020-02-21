using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Project2
{
    class DBQuery
    {
        private static DBQuery _databaseConnexion;
        private static SqlConnection _sqlConnexion;

        private DBQuery()
        {
            ConnectionStringSettings connString = ConfigurationManager.ConnectionStrings["SqlConnection"];
            string connectionString = connString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            IO.DisplayInformation("Getting Connection ...");
            _sqlConnexion = conn;

            try
            {
                IO.DisplayInformation("Openning Connection ...");
                conn.Open();
                IO.DisplayInformation("Connection successful!");
            }
            catch (Exception e)
            {
                IO.DisplayWarning("Error: " + e.Message);
            }

        }

        ~DBQuery()
        {
            GetConnexion.Close();
            GetConnexion.Dispose();
        }

        public static SqlConnection GetConnexion
        {
            get
            {
                if (_databaseConnexion == null)
                {
                    _databaseConnexion = new DBQuery();
                }
                return _sqlConnexion;
            }
        }


        public static Customer GetCustomerFromDB(string field, string value)
        {
            string sql = "SELECT idCustomer,login,name,password, location FROM Customer WHERE " + field  + " = @value";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = GetConnexion;
            cmd.CommandText = sql;

            IEnumerable<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@value", value)
            };
            foreach (SqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }

            Customer currentClient = null;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        currentClient  = new Customer(reader);
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
                    + " VALUES (@name, @login , @password , @location)";

            name = name != null ? name : ""; 
            location = location != null ? location : "";

            IEnumerable<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@name", name),
                new SqlParameter("@login", login),
                new SqlParameter("@password", password),
                new SqlParameter("@location", location)

             };

            ExecuteQuery(sql, parameters);

        }

        public static void SaveNewAccountInDb(Account account)
        {
            string typeOfAccount;
            Type type = account.GetType();
            /*if (type.Name == "CheckingAccount")
            {
                typeOfAccount = "CA";
            }
            else
            {
                typeOfAccount = "SA";
            }*/
            typeOfAccount = (type.Name == "CheckingAccount") ? "CA" : "SA";

            string sql = "INSERT INTO Account (idCustomer,accountNumber,amount,type) "
                        + " VALUES (@idCustomer,@accountNumber,@amount,@type);SELECT CAST(SCOPE_IDENTITY() AS int)";

            IEnumerable<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@idCustomer", account.IdCustomer),
                new SqlParameter("@accountNumber", account.AccountNumber),
                new SqlParameter("@amount", account.Amount),
                new SqlParameter("@type", typeOfAccount)

             };

            int lastIdInserted = ExecuteQueryWithID(sql, parameters);
            UpdateAccountNumberInAccount(lastIdInserted, "FR" + lastIdInserted);

        }

        public static void SaveNewTransferInDb(List<TransfertMoney> transfertList)
        {
            foreach (TransfertMoney transfert in transfertList)
            {
                string sql = "INSERT INTO Transfert (idOriginAccount,idDestinationAccount,amount,transferDate, isDone, idTransaction) "
                        + " VALUES (@idOriginAccount,@idDestinationAccount,@amount,@transferDate, 0, @idTransaction)";

                IEnumerable<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter ("@idOriginAccount", transfert.IdOrigin),
                    new SqlParameter ("@idDestinationAccount", transfert.idDestination),
                    new SqlParameter ("@amount", transfert.Amount),
                    new SqlParameter ("@transferDate", transfert.TransfertDate),
                    new SqlParameter ("@idTransaction", transfert.IdTransaction)
                };
                ExecuteQuery(sql,parameters);
            }
        }

        public static int GetIdCustomerFromAccountNumber(string accountNumber)
        {
            string sql = "SELECT [idCustomer] FROM Account WHERE accountNumber = @accountNumber";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = GetConnexion;
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("@accountNumber", accountNumber));

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
                            "FROM [Account] WHERE accountNumber = @accountNumber";


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = GetConnexion;
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("@accountNumber", accountNumber));

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
            Account resultAccount = AccountFactory.Create(type);

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
            string sql = "SELECT COUNT([idCustomer]) AS nbCustomer FROM AccountAuthorizedCustomers WHERE idAccount = '@idAccount' " +
                         "AND idCustomer = '@idCustomer'";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = GetConnexion;
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("@idAccount", account.IdAccount));
            cmd.Parameters.Add(new SqlParameter("@idCustomer", Program.currentCustomer.IdCustomer));

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
            string sql = "UPDATE Account SET amount = '@amount' WHERE idAccount = '@idAccount'";

            IEnumerable<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@amount", account.Amount),
                new SqlParameter("@idAccount", account.IdAccount)
             };

            ExecuteQuery(sql, parameters);
        }

        public static void UpdateAccountNumberInAccount(int idAccount, String accountNumber)
        {
            string sql = "UPDATE Account SET accountNumber = @AccountNumber WHERE idAccount = @idAccount";

            IEnumerable<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@AccountNumber", accountNumber),
                new SqlParameter("@idAccount", idAccount)
             };

            ExecuteQuery(sql, parameters);
        }

        public static int InsertTransaction(Transaction currentTransaction)
        {
            string transactionType = currentTransaction.GetType().Name;
            DateTime startDateString = currentTransaction.StartDate;
            DateTime endDateString = currentTransaction.EndDate;
            int Periodicity= currentTransaction.Periodicity;
            DateTime TransferDate = currentTransaction.TransferDate;

            string sql;

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@AccountOrigin", currentTransaction.AccountOrigin),
                new SqlParameter("@AccountDestination", currentTransaction.AccountDestination),
                new SqlParameter("@Amount", currentTransaction.Amount),
                new SqlParameter("@transactionType", transactionType),
                
             };

            if (currentTransaction.GetType().Name == "Instant" || currentTransaction.GetType().Name == "Deferred")
            {
                parameters.Add(new SqlParameter("@TransferDate", TransferDate));
                sql = "INSERT INTO[Transaction] " +
                    "(idOriginAccount, idDestinationAccount, amount, transactionType, transactionDate, transferDate) VALUES(@AccountOrigin , @AccountDestination, @Amount,@transactionType, GetDate(), @TransferDate);SELECT CAST(SCOPE_IDENTITY() AS int)";

            }
            else
            {
                parameters.Add(new SqlParameter("@startDateString", startDateString));
                parameters.Add(new SqlParameter("@endDateString", endDateString));
                parameters.Add(new SqlParameter("@Periodicity", Periodicity));
                sql = "INSERT INTO[Transaction] " +
                    "(idOriginAccount, idDestinationAccount, amount, transactionType, transactionDate, beginDate, endDate, periodicity) VALUES(@AccountOrigin , @AccountDestination, @Amount,@transactionType, GetDate(), @startDateString, @endDateString,@Periodicity);SELECT CAST(SCOPE_IDENTITY() AS int)";
            }

            return ExecuteQueryWithID(sql, parameters);
        }

        public static List<Account> GetAccountsCustomer(int idCustomer)
        {
            string sql = " SELECT[idAccount],[idCustomer],[accountNumber],[amount],[type],[isDebitAuthorized],[creationDate] FROM Account " +
                            "WHERE idCustomer = @idCustomer";

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = GetConnexion;
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("@idCustomer", idCustomer));

            List<Account> ListAccountsCustomer = new List<Account>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        string typeAccount = reader.GetString(reader.GetOrdinal("type")); ;
                        Account currentCustomerAccount = AccountFactory.Create(typeAccount);

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
            cmd.Connection = GetConnexion;
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

        private static void ExecuteQuery(string sql, IEnumerable<SqlParameter> parameters = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = GetConnexion;
            cmd.CommandText = sql;

            if(parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            cmd.ExecuteNonQuery();
        }

        private static Int32 ExecuteQueryWithID(string sql, IEnumerable<SqlParameter> parameters = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = GetConnexion;
            cmd.CommandText = sql;

            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            return (Int32)cmd.ExecuteScalar();
        }
    }
}
