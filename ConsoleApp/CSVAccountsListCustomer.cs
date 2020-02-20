using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;

namespace Project2
{
    public class CSV : ClassMap<Account>
    {
        public CSV()
        {
            Map(c => c.IdAccount).Index(0);
            Map(c => c.IdCustomer).Index(1);
            Map(c => c.AccountNumber).Index(2);
            Map(c => c.Amount).Index(3);
            Map(c => c.IsDebitAuthorized).Index(4);
            Map(c => c.CreationDate).Index(5);
        }
    }
}
