using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class SavingsAccount : AbstractAccount
    {
        public override decimal Ceiling 
        {
            get { return 1000; }
        } 

        public override double SavingsRate 
        {
            get { return 0.10; } 
        }

        public override decimal Overdraft
        {
            get { return 0; }
        }

        public SavingsAccount(string accountIdentifier, decimal amount, int idClient) : base(accountIdentifier,amount,idClient)
        {
            AccountNumber = accountIdentifier;
            Amount = amount;
            IdCustomer = idClient;
        }

        public SavingsAccount()
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
            if (Customer.IsAccountOwner(Program.currentCustomer.IdCustomer, accountDestination.AccountNumber) == false)
            {
                throw new ArgumentException(this.AccountNumber + " can not credit " + accountDestination.AccountNumber);
            }

            if (amountToTransfer <= Amount && IsDebitAuthorized)
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
            if ((amountToTransfer + Amount) < Ceiling && IsAuthorizeCustomerToCredit())
            {
                return true;
            }
            return false;
        }

        public override bool IsAuthorizeCustomerToCredit()
        {
            if (DBQuery.IsCurrentCustomerAuthorizedOnAccount(this))
            {
                return true;
            }
            return false;
        }

        public override bool isDebitAuthorized(AbstractAccount accountDestination)
        {
            if(accountDestination.IsDebitAuthorized == true)
            {
                return true;
            }
            return false;
        }
    }
}
