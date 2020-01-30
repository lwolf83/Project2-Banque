using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class CheckingAccount : Account
    {
        public double Overdraft { get; set; }

        public CheckingAccount (string accountIdentifier, double amout, int idClient)
        {
            AccountIdentifier = accountIdentifier;
            Amount = amout;
            IdClient = idClient;
        }

    }
}
