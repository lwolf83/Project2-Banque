using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class Instant : Transaction
    {
        public override List<TransfertMoney> GetTransferts()
        {
            List<TransfertMoney> Transfert = new List<TransfertMoney>();
            TransfertMoney currentTransfert = new TransfertMoney(this);
            Transfert.Add(currentTransfert);
            return Transfert;
        }

    }
}
