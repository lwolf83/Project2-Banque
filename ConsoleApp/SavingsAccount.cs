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
    }
}
