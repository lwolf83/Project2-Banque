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
            DateTime startDate = DateTime.Now.AddSeconds(-86400);

            IEnumerable<Transaction> transactionToDo = Database.Instance.GetTransactions(startDate, endDate);
            foreach (Transaction transaction in transactionToDo)
            {

                foreach (Transfer transfer in transaction.Transfers)
                {
                    transaction.OriginAccount = Database.Instance.GetOriginAccountByTransaction(transaction);
                    transaction.DestinationAccount = Database.Instance.GetDestinaryAccountByTransaction(transaction);
                    if (!transfer.isDone)
                    {
                        if(Check(transaction))
                        {
                            transaction.OriginAccount.amount = transaction.OriginAccount.amount - transaction.amount;
                            transaction.DestinationAccount.amount = transaction.DestinationAccount.amount + transaction.amount;
                            transfer.isDone = true;
                            Database.Instance.UpdateAccount(transaction.OriginAccount);
                            Database.Instance.UpdateAccount(transaction.DestinationAccount);
                            Database.Instance.UpdateTransfer(transfer);
                        }

                    }
                }
                //Database.Instance.UpdateTransaction(transaction);

            }  
            
        }

        public static bool Check(Transaction transaction)
        {
            Logger.Debug("Control start on " + transaction.OriginAccount.type);
            if (transaction.OriginAccount.type == "CA" && transaction.DestinationAccount.type == "CA")
            {
                bool overdraftOK = transaction.OriginAccount.amount - transaction.amount > transaction.OriginAccount.overdraft;
                if (overdraftOK)
                {
                    return true;
                }
                else
                {
                    Logger.Debug(transaction.idTransaction + "can not be done (overdraft ko).");
                    return false;
                }
                
            }
            else if (transaction.OriginAccount.type == "CA" && transaction.DestinationAccount.type == "SA")
            {
                bool overdraftOK = transaction.OriginAccount.amount - transaction.amount > transaction.OriginAccount.overdraft;
                bool ceilingOk = transaction.DestinationAccount.amount + transaction.amount < transaction.DestinationAccount.ceiling;
                if (overdraftOK && ceilingOk)
                {
                    return true;
                }
                if(!overdraftOK)
                {
                    Logger.Debug(transaction.idTransaction + "can not be done (overdraft ko).");
                    return false;
                }
                else
                {
                    Logger.Debug(transaction.idTransaction + "can not be done (ceiling ko).");
                    return false;
                }
                
            }
            else if (transaction.OriginAccount.type == "SA" && transaction.DestinationAccount.type == "CA")
            {
                if(transaction.OriginAccount.isDebitAuthorized)
                {
                    return true;
                }
                else
                {
                    Logger.Debug(transaction.idTransaction + "can not be done (not authorized to debit).");
                    return false;
                }
                
            }
            else if (transaction.OriginAccount.type == "SA" && transaction.DestinationAccount.type == "SA")
            {
                bool sameOwner = transaction.OriginAccount.idCustomer == transaction.DestinationAccount.idCustomer;
                if (transaction.OriginAccount.isDebitAuthorized && sameOwner)
                {
                    return true;
                }
                else if(!transaction.OriginAccount.isDebitAuthorized)
                {
                    Logger.Debug(transaction.idTransaction + "can not be done (not authorized to debit).");
                    return false;
                }
                else
                {
                    Logger.Debug(transaction.idTransaction + "can not be done (not same owner).");
                    return false;
                }
                
            }
            else
            {
                throw new Exception("Oups something wrong with checking conditions");
            }
        }

    }
}
