using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;

namespace Project2
{

    class CSVTransfertList : ClassMap<TransferMoney>
    {
        public CSVTransfertList()
        {
            Map(c => c.IdOrigin).Index(0);
            Map(c => c.idDestination).Index(1);
            Map(c => c.TransferDate).Index(2);
            Map(c => c.Amount).Index(3);
            Map(c => c.IsDone).Index(4);
            Map(c => c.IdTransaction).Index(5);
        }
    }
}