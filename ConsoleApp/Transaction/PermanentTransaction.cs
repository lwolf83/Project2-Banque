using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class PermanentTransaction : AbstractTransaction
    {
        public static AbstractTransaction CreatePermanent()
        {
            return new PermanentTransaction();
        }

        public static AbstractTransaction CreatePermanent(DateTime? startDate, DateTime? endDate)
        {
            AbstractTransaction permanentTransaction = new PermanentTransaction();
            permanentTransaction.StartDate = startDate;
            permanentTransaction.EndDate = endDate;
            return permanentTransaction;
        }

        public override List<TransferMoney> GetTransferts()
        {
            List<TransferMoney> Transfert = new List<TransferMoney>();
            while (StartDate < EndDate)
            {
                TransferMoney currentTransfert = new TransferMoney(this);
                Transfert.Add(currentTransfert);
                StartDate = StartDate + TimeSpan.FromDays(Periodicity);
            }
            return Transfert;
        }

    }
}
