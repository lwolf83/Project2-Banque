using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class TransfertMoney
    {
        public int IdOrigin { get; set; }
        public int  idDestination { get; set; }
        public DateTime TransfertDate { get; set; }
        public Decimal Amount { get; set; }
        public bool IsDone { get; set; }
        public Int16 IdTransaction { get; set; }

        public TransfertMoney(Instant transaction)
        {
            IdOrigin = transaction.AccountOrigin;
            idDestination = transaction.AccountDestination;
            TransfertDate = transaction.TransferDate;
            Amount = transaction.Amount;
            IsDone = false;
            IdTransaction = IdTransaction;
        }

        public TransfertMoney(Deferred transaction)
        {
            IdOrigin = transaction.AccountOrigin;
            idDestination = transaction.AccountDestination;
            TransfertDate = transaction.TransferDate;
            Amount = transaction.Amount;
            IsDone = false;
            IdTransaction = IdTransaction;
        }


        public TransfertMoney(Permanent transaction)
        {
            IdOrigin = transaction.AccountOrigin;
            idDestination = transaction.AccountDestination;
            TransfertDate = transaction.StartDate;
            Amount = transaction.Amount;
            IsDone = false;
            IdTransaction = IdTransaction;
        }

    }
}
