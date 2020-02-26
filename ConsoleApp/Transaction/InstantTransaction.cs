using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class InstantTransaction : AbstractTransaction
    {
        public static AbstractTransaction Create()
        {
            AbstractTransaction instantTransaction = new InstantTransaction(); 
            instantTransaction.StartDate = DateTime.Now;
            instantTransaction.EndDate = DateTime.Now;
            return instantTransaction;
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
