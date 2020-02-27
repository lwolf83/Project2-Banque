using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JELListener
{
    class Transaction
    {
        public int Id { get; set; }
        public int DestinaryAccount { get; set; }
        public int OriginAccount { get; set; }
        public IEnumerable<Transfer> Transfers { get; set; }
    }
}
