using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class CheckingAccount : AbstractAccount
    {
        public override decimal Ceiling
        {
            get { return 0; }
            set { }
        }
        public override decimal SavingsRate
        {
            get; set;
        }
        public override decimal Overdraft
        {
            get { return -200; }
            set { }
        }    

        public CheckingAccount (string accountIdentifier, decimal amount, int idClient) : base(accountIdentifier, amount, idClient)
        {
            AccountNumber = accountIdentifier;
            Amount = amount;
            IdCustomer = idClient;
        }

        public CheckingAccount()
        {
        }

        public override void Debit()
        {
        }

        public override void Credit()
        {
        }

        public override bool CanBeDebited(decimal amountToTransfer, AbstractAccount accountDestination)
        {
            if ((Amount - Overdraft) >= amountToTransfer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool CanBeCredited(decimal amountToTransfer)
        {
            return true;
        }

        public override bool IsAuthorizeCustomerToCredit()
        {
            return true;
        }

        public override bool isDebitAuthorized(AbstractAccount accountDestination)
        {
            if (IdCustomer == Program.currentCustomer.IdCustomer)
            {
                return true;
            }
            return false;
        }

    }
}
