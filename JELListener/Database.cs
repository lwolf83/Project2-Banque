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
            throw new NotImplementedException("Not implemented yet");
        }

        public Account GetOriginAccountByTransaction(Transaction transaction)
        {
            throw new NotImplementedException("Récupérer le compte d'origine d'une transaction");
        }

        public Account GetDestinaryAccountByTransaction(Transaction transaction)
        {
            throw new NotImplementedException("Récupérer le compte destinataire");
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
