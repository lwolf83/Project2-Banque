using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class Permanent : Transaction
    {
        private string _typeOfPeriodicity; // day, month, year, each
        private int _periodicity;
        private DateTime _startDate;
        private DateTime _endDate;

        public string GetTypeOfPeriodicity()
        {
            return _typeOfPeriodicity;
        }

        public void SetTypeOfPeriodicity(string typeOfPeriodicity)
        {
            _typeOfPeriodicity = typeOfPeriodicity;
        }

        public int GetPeriodicity()
        {
            return _periodicity;
        }

        public void SetPeriodicity(int periodicity)
        {
            _periodicity = periodicity;
        }

        public DateTime GetStartDate()
        {
            return _startDate;
        }

        public void SetStartDate(DateTime startDate)
        {
            _startDate = startDate;
        }

        public DateTime GetEndDate()
        {
            return _endDate;
        }

        public void SetEndDate(DateTime endDate)
        {
            _endDate = endDate;
        }
    }
}
