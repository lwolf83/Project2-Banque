using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JELListener
{
    class Transfer
    {
        public int idTransfert { get; set; }
        public int idOriginAccount { get; set; }
        public int idDestinationAccount { get; set; }
        public Decimal amount { get; set; }
        public DateTime transferDate { get; set; }
        public bool isDone { get; set; }
        public int idTransaction { get; set; }
        public Transaction Transaction { get; set; }
    }
}
