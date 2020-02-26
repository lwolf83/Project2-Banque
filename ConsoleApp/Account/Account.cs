using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public abstract class AbstractAccount
    {
        public int IdAccount { get; set; }
        public  int IdCustomer { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public bool IsDebitAuthorized { get; set; }
        public DateTime CreationDate { get; set; }
        public abstract decimal Overdraft { get; set; }
        public abstract decimal Ceiling { get; set; }
        public abstract decimal SavingsRate { get; set; }

        public AbstractAccount (string accountIdentifier, decimal amount, int idClient)
        {
            AccountNumber = accountIdentifier;
            Amount = amount;
            IdCustomer = idClient;
        }
        public AbstractAccount()
        {
        }

        public abstract void Debit();

        public abstract void Credit();

        public abstract bool CanBeDebited(decimal amountToTransfer, AbstractAccount accountDestination);

        public abstract bool CanBeCredited(decimal amountToTransfer);

        public abstract bool IsAuthorizeCustomerToCredit();

        public abstract bool isDebitAuthorized(AbstractAccount accountDestination);
    }
}