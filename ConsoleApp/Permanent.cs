using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class Permanent : Transaction
    {
        public string TypeOfPeriodicity { get; set; } // day, month, year, each
        public int Periodicity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

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
