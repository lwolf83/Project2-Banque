using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class Account
    {
        protected int _idClient;
        protected string _accountIdentifier;
        protected double _amount;
        protected DateTime _creationDate;

       

        public int GetIdClient()
        {
            return _idClient;
        }

        public void SetIdClient(int idClient)
        {
            _idClient = idClient;
        }

        public string GetAccountIdentifier()
        {
            return _accountIdentifier;
        }

        public void SetAccountIdentifier(string accountIdentifier)
        {
            _accountIdentifier = accountIdentifier;
        }

        public double GetAmount()
        {
            return _amount;
        }

        public void SetAmount(double amount)
        {
            _amount = amount;
        }

        public virtual void CreateAccount()
        {
            //assigner le compte à un client --> à faire dans un constructeur 
        }

        public virtual void Debit()
        {
        }

        public virtual void Credit()
        {
        }

        public virtual bool IsValidDebit(double amount)
        {
            return true;
        }

        public virtual bool IsValidCredit(double amount)
        {
            return true;
        }

        
    }
}