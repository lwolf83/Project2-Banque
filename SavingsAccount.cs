using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class SavingsAccount : Account
    {
        private double _ceiling;
        private double _savingsRate;

        public SavingsAccount(string accountIdentifier, double amount, int idClient)
        {
            _accountIdentifier = accountIdentifier;
            _amount = amount;
            _idClient = idClient;
        }

        public SavingsAccount(int idClient)
        {
            _idClient = idClient;
            _amount = 0;
        }


        public double GetCeiling()
        {
            return _ceiling;
        }

        public double GetSavingsRate()
        {
            return _savingsRate;
        }
    }
}
