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

        public SavingsAccount(string accountIdentifier, double amount, int idClient)
        {
            AccountIdentifier = accountIdentifier;
            Amount = amount;
            IdClient = idClient;
        }

        public SavingsAccount(int idClient)
        {
            IdClient = idClient;
            Amount = 0;
        }
    }
}
