using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class Transfert
    {
        public Account Origin { get; set; }
        public Account Destination { get; set; }
        public DateTime TransfertDate { get; set; }
        public Decimal Amount { get; set; }
        public bool IsDone { get; set; }
        public Int16 IdTransaction { get; set; }
    }
}
