using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JELListener
{
    class TransferExecutor
    {
        public static void ExecuteTransfers()
        {
            
            Database database = Database.Instance;
            DateTime endDate = DateTime.Now;
            DateTime startDate = DateTime.Now.AddSeconds(-15);

            IEnumerable<Transaction> transactionToDo = Database.Instance.GetTransactions(startDate, endDate);
            foreach(Transaction transaction in transactionToDo)
            {
                Check(transaction);
                foreach (Transfer transfer in transaction.Transfers)
                {
                    Execute(transfer);
                }
                Database.Instance.UpdateTransaction(transaction);

            }
        }

        public static void Check(Transaction transaction)
        {
           
        }

        public static void Execute(Transfer transfer)
        {

        }

    }
}
