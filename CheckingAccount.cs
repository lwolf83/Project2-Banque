using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class CheckingAccount : Account
    {
        private double _overdraft;

        public CheckingAccount (string accountIdentifier, double amout, int idClient)
        {
            _accountIdentifier = accountIdentifier;
            _amount = amout;
            _idClient = idClient;
        }

        public double GetOverdraft()
        {
            return _overdraft;
        }

    }
}
