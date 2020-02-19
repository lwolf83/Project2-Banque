using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class CheckingAccount : Account
    {
        public override decimal Ceiling
        {
            get { return 0; }
        }
        public override double SavingsRate
        {
            get { return 0; }
        }
        public override decimal Overdraft
        {
            get { return -200; }
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

        public override bool CanBeDebited(decimal amountToTransfer, Account accountDestination)
        {
            Account currentAccount = DBQuery.GetAccountFromDB(AccountNumber);

            if (currentAccount.Amount > Overdraft)
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

        public override bool isDebitAuthorized(Account accountDestination)
        {
            if (IdCustomer == Program.currentCustomer.IdCustomer)
            {
                return true;
            }
            return false;
        }

    }
}
