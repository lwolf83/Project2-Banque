using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class SavingsAccount : AbstractAccount
    {
        public override decimal Ceiling 
        {
            get;
            set;
        } 

        public override decimal SavingsRate 
        {
            get;
            set;
        }

        public override decimal Overdraft
        {
            get { return 0; }
            set { }
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
            if (isDebitAuthorized(this))
            {
                return true;
            }
            else if (Customer.IsAccountOwner(Program.currentCustomer.IdCustomer, accountDestination.AccountNumber) == false)
            {
                IO.DisplayWarning(this.AccountNumber + " can not credit " + accountDestination.AccountNumber);
                return false;
                
            }
            else
            {
                IO.DisplayWarning(this.AccountNumber + " is not authorized to debit " + accountDestination.AccountNumber);
                return false;
            }
        }

        public override bool CanBeCredited(decimal amountToTransfer)
        {
            if (IsAuthorizeCustomerToCredit())
            {
                return true;
            }
            IO.DisplayWarning("You are not authorized to credit a saving account that is not yours.");
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

        public override bool isDebitAuthorized(AbstractAccount accountOrigin)
        {
            if(accountOrigin.IsDebitAuthorized == true)
            {
                return true;
            }
            return false;
        }

        public override bool isMoneyEnough(decimal amountToTransfer)
        {
            if (amountToTransfer <= Amount)
            {
                return true;
            }
            else
            {
                IO.DisplayWarning(this.AccountNumber + " has not sufficient funds to do this transaction.");
                return false;
            }
           
        }

        public override bool isTransferNotReachingCeiling(decimal amountToTransfer)
        {
            if ((amountToTransfer + Amount) < Ceiling)
            {
                return true;
            }
            else
            {
                IO.DisplayWarning(amountToTransfer + " cannot be credited because it will exceed the ceiling of the destination account.");
                return false;
            }
        }


    }
}
