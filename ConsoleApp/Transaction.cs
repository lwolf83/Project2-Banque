using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class Transaction
    {
        public int IdTransaction { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public int AccountOrigin { get; set; }
        public int AccountDestination { get; set; }
    }
}