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

        public IEnumerable<Transaction> GetTransactions(DateTime beginDate, DateTime? endDate)
        {
            string sql = "SELECT [idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [beginDate], [endDate], [periodicity] FROM Transaction WHERE beginDate >= @beginDate AND endDate <= @endDate";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Database.Instance._connection;
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("@beginDate", beginDate));
            cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
            List<Transaction> newTransactiontList = new List<Transaction>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        String transactionType = reader.GetString(reader.GetOrdinal("transactionType"));
                        Transaction transaction = new Transaction();
                        transaction.idTransaction = reader.GetInt32(reader.GetOrdinal("idTransaction"));
                        transaction.idOriginAccount = reader.GetInt32(reader.GetOrdinal("idOriginAccount"));
                        transaction.idDestinationAccount = reader.GetInt32(reader.GetOrdinal("idDestinationAccount"));
                        transaction.amount = reader.GetDecimal(reader.GetOrdinal("amount"));
                        transaction.transactionDate = reader.GetDateTime(reader.GetOrdinal("transactionDate"));
                        transaction.beginDate = reader.GetDateTime(reader.GetOrdinal("beginDate"));
                        transaction.endDate = reader.GetDateTime(reader.GetOrdinal("endDate"));
                        transaction.periodicity = reader.GetInt32(reader.GetOrdinal("periodicity"));
                        newTransactiontList.Add(transaction);
                    }
                }
            }
            foreach (Transaction transaction in newTransactiontList)
            {
                transaction.Transfer = Database.Instance.GetTransfersByTransaction(transaction);
            }
            return newTransactiontList;
        }

        public IEnumerable<Transfer> GetTransfersByTransaction(Transaction transaction)
        {
            string sql = "SELECT * FROM Transfert WHERE idTransaction = @idTransaction";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("@idTransaction", transaction));
            List<Transfer> TransfertList = new List<Transfer>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Transfer transfertMoney = new Transfer();
                        transfertMoney.idTransfert = reader.GetInt32(reader.GetOrdinal("idTransfert"));
                        transfertMoney.idOriginAccount = reader.GetInt32(reader.GetOrdinal("idOriginAccount"));
                        transfertMoney.idDestinationAccount = reader.GetInt32(reader.GetOrdinal("idDestinationAccount"));
                        transfertMoney.amount = reader.GetDecimal(reader.GetOrdinal("amount"));
                        transfertMoney.transferDate = reader.GetDateTime(reader.GetOrdinal("transferDate"));
                        transfertMoney.isDone = reader.GetBoolean(reader.GetOrdinal("isDone"));
                        transfertMoney.idTransaction = reader.GetInt32(reader.GetOrdinal("idTransaction"));
                        TransfertList.Add(transfertMoney);
                    }
                }
                return TransfertList;
            }
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
            string sql = "UPDATE Transfert SET isDone WHERE idTransfert = @idTransfert";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("@idTransfert", transfer));
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Transfer updateTransfer = new Transfer();
                        updateTransfer.idTransfert = reader.GetInt32(reader.GetOrdinal("idTransfert"));
                        updateTransfer.idOriginAccount = reader.GetInt32(reader.GetOrdinal("idOriginAccount"));
                        updateTransfer.idDestinationAccount = reader.GetInt32(reader.GetOrdinal("idDestinationAccount"));
                        updateTransfer.amount = reader.GetDecimal(reader.GetOrdinal("amount"));
                        updateTransfer.transferDate = reader.GetDateTime(reader.GetOrdinal("transferDate"));
                        updateTransfer.isDone = reader.GetBoolean(reader.GetOrdinal("isDone"));
                        updateTransfer.idTransaction = reader.GetInt32(reader.GetOrdinal("idTransaction"));
                    }
                }
            }
        }

        public void UpdateAccount(Account account)
        {
            


            throw new NotImplementedException("Mettre à jour le compte en fonction du paramètre account");
        }
    }
}
