using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class Transaction
    {
        private int _idTransaction;
        private DateTime _requestDate;
        private DateTime _transactionDate;
        private double _amount;
        private Account _accountOrigin;
        private Account _accountDestination;
        private bool _isDone;

        public int GetIdTransaction()
        {
            return _idTransaction;
        }

        public void SetIdTransaction(int idTransaction)
        {
            _idTransaction = idTransaction;
        }

        public DateTime GetRequestDate()
        {
            return _requestDate;
        }
        
        public void SetRequestDate(DateTime requestDate)
        {
            _requestDate = requestDate;
        }

        public DateTime GetTransactionDate()
        {
            return _transactionDate;
        }

        public void GetTransactionDate(DateTime transactionDate)
        {
            _transactionDate = transactionDate;
        }

        public double GetAmount()
        {
            return _amount;
        }

        public void SetAmount(Double amount)
        {
            _amount = amount;
        }

        public Account GetAccountOrigin()
        {
            return _accountOrigin;
        }

        public void SetAccountOrigin(Account accountOrigin)
        {
            _accountOrigin = accountOrigin;
        }

        public Account GetAccountDestination()
        {
            return _accountDestination;
        }

        public void SetAccountDestination(Account accountDestination)
        {
            _accountDestination = accountDestination;
        }

        public bool GetIsDone()
        {
            return _isDone;
        }

        public void SetIsDone(bool isDone)
        {
            _isDone = isDone;
        }
    }
}