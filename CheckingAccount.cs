using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class CheckingAccount : Account
    {
        private double _overdraft;

        public double GetOverdraft()
        {
            return _overdraft;
        }

    }
}
