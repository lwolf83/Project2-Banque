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
        public double Amount { get; set; }
        public Account AccountOrigin { get; set; }
        public Account AccountDestination { get; set; }
        public bool IsDone { get; set; }
    }
}