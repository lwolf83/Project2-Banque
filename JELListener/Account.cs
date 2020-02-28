using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JELListener
{

    public class Account
    {
        public int idAccount { get; set; }
        public int idCustomer { get; set; }
        public string accountNumber { get; set; }
        public decimal amount { get; set; }
        public string type { get; set; }
        public bool isDebitAuthorized { get; set; }
        public DateTime creationDate { get; set; }
        public decimal ceiling { get; set; }
        public decimal overdraft { get; set; }
        public decimal savingsRate { get; set; }


    }
}
