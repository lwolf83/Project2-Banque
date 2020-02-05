using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class CheckingAccount : Account
    {
        public decimal Overdraft { get; set; } = -200;

        public CheckingAccount()
        {

        }

        public CheckingAccount (string accountIdentifier, decimal amout, int idClient)
        {
            AccountNumber = accountIdentifier;
            Amount = amout;
            IdCustomer = idClient;
        }

        public override bool CanBeDebited(decimal AmountToTransfer)
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
    }
}
