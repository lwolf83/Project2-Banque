using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class Permanent : Transaction
    {
        public string TypeOfPeriodicity { get; set; } // day, month, year, each
        public int Periodicity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
