using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;

namespace Project2
{
    class CSVTransaction
    {
        public string IdTransaction { get; set; }
    }
    class CSVTransactionCustomer : ClassMap<AbstractTransaction>
    {
        public CSVTransactionCustomer()
        {
            Map(c => c.IdTransaction).Index(0);
            Map(c => c.TransactionDate).Index(1);
            Map(c => c.Amount).Index(3);
            Map(c => c.AccountOrigin).Index(4);
            Map(c => c.AccountDestination).Index(5);
            Map(c => c.Periodicity).Index(6);
            Map(c => c.StartDate).Index(7);
            Map(c => c.EndDate).Index(8);        
        }
    }
}
