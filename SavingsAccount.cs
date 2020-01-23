using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class SavingsAccount : Account
    {
        private double _ceiling;
        private double _savingsRate;

        public SavingsAccount(string accountIdentifier, double amout, int idClient)
        {
            _accountIdentifier = accountIdentifier;
            _amount = amout;
            _idClient = idClient;
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
