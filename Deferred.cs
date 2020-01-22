using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class Deferred : Transaction
    {
        private DateTime _dateTransaction;

        public DateTime GetDateTransaction()
        {
            return _dateTransaction;
        }

        public void SetDateTransaction(DateTime dateTransaction)
        {
            _dateTransaction = dateTransaction;
        }
    }
}
