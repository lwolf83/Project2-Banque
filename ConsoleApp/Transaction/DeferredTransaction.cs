using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class DeferredTransaction : AbstractTransaction
    {
        public static DeferredTransaction CreateDeferred()
        {
            return new DeferredTransaction();
        }

        public static AbstractTransaction CreateDeferred(DateTime? startDate)
        {
            AbstractTransaction deferredTransaction = new DeferredTransaction();
            deferredTransaction.StartDate = startDate;
            deferredTransaction.EndDate = startDate;
            return deferredTransaction;
        }
        public override List<TransferMoney> GetTransferts()
        {
            List<TransferMoney> Transfert = new List<TransferMoney>();
            TransferMoney currentTransfert = new TransferMoney(this);
            Transfert.Add(currentTransfert);
            return Transfert;
        }
    }
}