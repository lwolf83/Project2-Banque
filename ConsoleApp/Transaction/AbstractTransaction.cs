using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public abstract class AbstractTransaction
    {
        public int IdTransaction { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public int AccountOrigin { get; set; }
        public int AccountDestination { get; set; }
        public string TypeOfPeriodicity { get; set; } // day, month, year, each
        public int Periodicity { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public abstract List<TransferMoney> GetTransferts();

        public static AbstractTransaction Create(String transactionType)
        {
            AbstractTransaction transaction;
            if (typeof(InstantTransaction).Name == transactionType)
            {
                transaction = InstantTransaction.Create();
            }
            else if (typeof(DeferredTransaction).Name == transactionType)
            {
                transaction = DeferredTransaction.Create();
            }
            else if (typeof(PermanentTransaction).Name == transactionType)
            {
                transaction = PermanentTransaction.Create();
            }
            else
            {
                throw new ArgumentException($"No transaction type corresponding to {transactionType}");
            }
            return transaction;
        }

        public static AbstractTransaction Create(DateTime? startDate = null, DateTime? endDate = null)
        {
            AbstractTransaction transaction = null;
            if (startDate == null)
            {
                transaction = InstantTransaction.Create();
            }
            else if(startDate > DateTime.Now && endDate == null)
            {
                transaction = DeferredTransaction.CreateDeferred(startDate);
            }
            else if (startDate < endDate)
            {
                transaction = PermanentTransaction.CreatePermanent(startDate, endDate);
            }
            else
            {
                throw new ArgumentException("No transaction can be executed from date " + startDate + " to " + endDate);
            }
            return transaction;
        }
    }
}