using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class SavingsAccount : Account
    {
        private double _ceiling;
        private double _savingsRate;

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
