using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class Permanent : Transaction
    {
        public override List<TransfertMoney> GetTransferts()
        {
            List<TransfertMoney> Transfert = new List<TransfertMoney>();
            while(StartDate < EndDate)
            {
                TransfertMoney currentTransfert = new TransfertMoney(this);
                Transfert.Add(currentTransfert);
                StartDate.AddDays(Periodicity);
            }
            return Transfert;
        }

    }
}
