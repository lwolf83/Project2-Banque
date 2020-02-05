using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class SavingsAccount : Account
    {
        public decimal Ceiling { get; set; } = 1000;
        public double SavingsRate { get; set; }

        public SavingsAccount()
        {
        }

        public SavingsAccount(string accountIdentifier, decimal amount, int idClient)
        {
            AccountNumber = accountIdentifier;
            Amount = amount;
            IdCustomer = idClient;
        }

        public SavingsAccount(int idClient)
        {
            IdCustomer = idClient;
            Amount = 0;
        }

        public override bool IsAuthorizeCustomerToCredit()
        {
            if(DBQuery.IsCurrentCustomerAuthorizedOnAccount(this) || (IdCustomer == Program.currentCustomer.IdCustomer))
            {
                return true;
            }
            return false;
        }

        public override bool CanBeCredited(decimal amountToTransfer)
        {            
            if ((amountToTransfer + Amount) < Ceiling && IsAuthorizeCustomerToCredit())
            {
                return true;
            }
            return false;
        }
    }
}
