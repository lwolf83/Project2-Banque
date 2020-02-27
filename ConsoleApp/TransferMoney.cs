using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class TransferMoney
    {
        public int idTransfert { get; set; }
        public int IdOrigin { get; set; }
        public int idDestination { get; set; }
        public DateTime? TransferDate { get; set; }
        public Decimal Amount { get; set; }
        public bool IsDone { get; set; }
        public int IdTransaction { get; set; }

        public TransferMoney()
        { }
        public TransferMoney(AbstractTransaction transaction)
        {
            IdOrigin = transaction.AccountOrigin;
            idDestination = transaction.AccountDestination;
            Amount = transaction.Amount;
            IsDone = false;
            IdTransaction = transaction.IdTransaction;
            TransferDate = transaction.StartDate;
        }

        public TransferMoney(PermanentTransaction transaction) : this((AbstractTransaction)transaction)
        {
            TransferDate = transaction.StartDate;
        }
    }
}
