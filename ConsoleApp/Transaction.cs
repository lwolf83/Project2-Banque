using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class Transaction
    {
        public int IdTransaction { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime TransferDate { get; set; }
        public decimal Amount { get; set; }
        public int AccountOrigin { get; set; }
        public int AccountDestination { get; set; }
        public string TypeOfPeriodicity { get; set; } // day, month, year, each
        public int Periodicity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}