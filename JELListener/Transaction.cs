using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JELListener
{
    class Transaction
    {
        public int idTransaction { get; set; }
        public int idOriginAccount { get; set; }
        public int idDestinationAccount { get; set; }
        public decimal amount { get; set; }
        public string transactionType { get; set; }
        public DateTime transactionDate { get; set; }
        public DateTime? beginDate { get; set; }
        public DateTime? endDate { get; set; }
        public int periodicity { get; set; }
        public IEnumerable<Transfer> Transfer { get; set; }
    }
}


