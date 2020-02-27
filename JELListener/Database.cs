using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

namespace JELListener
{
    class Database
    {
        private static Database _instance = null;
        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }

        private SqlConnection _connection = null;

        public void Connect(SqlConnectionStringBuilder builder)
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                throw new Exception("Database already connected");
            }
            _connection.ConnectionString = builder.ConnectionString;
            _connection.Open();
        }

        public IEnumerable<Transaction> GetTransactions(DateTime startDate, DateTime? endDate)
        {
            throw new NotImplementedException("Retourne toutes les transaction avec les comptes associés (Account) et les transferts associés");
        }

        public IEnumerable<Transfer> GetTransfersByTransaction(Transaction transaction)
        {
            throw new NotImplementedException("Not implemented yet");
        }

        public Account GetAccountByTransaction(int Account)
        {
            string sql = "SELECT [idAccount],[idCustomer],[accountNumber],[amount],[type],[isDebitAuthorized],[creationDate],[ceiling],[overdraft],[savingsRate]  " +
                            "FROM [Account] WHERE idAccount = @idAccount";


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Database.Instance._connection;
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("@idAccount", Account));

            int idAccount = 0;
            int idCustomer = 0;
            string accountNumber = "";
            decimal amount = 0;
            string type = "";
            bool isDebitAuthorized = true;
            DateTime creationDate = new DateTime();
            decimal ceiling = 0;
            decimal overdraft = 0;
            decimal savingsRate = 0;


            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    idAccount = reader.GetInt32(reader.GetOrdinal("idAccount"));
                    idCustomer = reader.GetInt32(reader.GetOrdinal("idCustomer"));
                    accountNumber = reader.GetString(reader.GetOrdinal("accountNumber"));
                    amount = reader.GetDecimal(reader.GetOrdinal("amount"));
                    type = reader.GetString(reader.GetOrdinal("type"));
                    isDebitAuthorized = reader.GetBoolean(reader.GetOrdinal("isDebitAuthorized"));
                    creationDate = reader.GetDateTime(reader.GetOrdinal("creationDate"));
                    ceiling = reader.GetDecimal(reader.GetOrdinal("ceiling"));
                    overdraft = reader.GetDecimal(reader.GetOrdinal("overdraft"));
                    savingsRate = reader.GetDecimal(reader.GetOrdinal("savingsRate"));
                }
            }
            Account resultAccount = new Account();
            resultAccount.accountNumber = accountNumber;
            resultAccount.amount = amount;
            resultAccount.creationDate = creationDate;
            resultAccount.idAccount = idAccount;
            resultAccount.idCustomer = idCustomer;
            resultAccount.isDebitAuthorized = isDebitAuthorized;
            resultAccount.ceiling = ceiling;
            resultAccount.overdraft = overdraft;
            resultAccount.savingsRate = savingsRate;
            return resultAccount;

        }

        public Account GetOriginAccountByTransaction(Transaction transaction)
        {
            return GetAccountByTransaction(transaction.idOriginAccount);
        }

        public Account GetDestinaryAccountByTransaction(Transaction transaction)
        {
            return GetAccountByTransaction(transaction.idDestinationAccount);
        }

        public void UpdateTransaction(Transaction transaction)
        {
            throw new NotImplementedException("Mettre à jour la transaction et TOUS ses transfert associés en base");
        }
        
        public void UpdateTransfer(Transfer transfer)
        {
            throw new NotImplementedException("Mettre à jour un transfer en base");
        }

        public void UpdateAccount(Account account)
        {
            


            throw new NotImplementedException("Mettre à jour le compte en fonction du paramètre account");
        }

    }
}
