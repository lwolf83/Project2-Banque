using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class CheckingAccount : Account
    {
        public double Overdraft { get; set; }

        public CheckingAccount()
        {

        }

        public CheckingAccount (string accountIdentifier, decimal amout, int idClient)
        {
            AccountNumber = accountIdentifier;
            Amount = amout;
            IdCustomer = idClient;
        }

    }
}
