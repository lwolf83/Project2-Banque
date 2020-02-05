using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class SavingsAccount : Account
    {
        public double Ceiling { get; set; }
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

        public override bool CanBeDebited(decimal amountToTransfer, Account accountDestination)
        {

            if (amountToTransfer <= Amount && IdCustomer == accountDestination.IdCustomer && IsDebitAuthorized)
            {
                return true;
            }
            else
            {
                 return false;
            }
        }

        /* public override bool isDebitAuthorized(Account accountDestination)
        {  // vérifier que le compte de destination est un compte de AccountOwner
            SavingsAccount savingsAccount = new SavingsAccount();
            accountDestination = DBQuery.GetAccountFromDB(AccountNumber);

            if (accountDestination.IdCustomer == savingsAccount.IdCustomer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/

    }
}
