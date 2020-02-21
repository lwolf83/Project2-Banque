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
            Map(c => c.AccountNumber).Index(0);
            Map(c => c.Amount).Index(1);
            Map(c => c.CreationDate).Index(2);
        }
    }
}
