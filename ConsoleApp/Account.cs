using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class Account
    {
        protected int IdClient { get; set; }
        protected string AccountIdentifier { get; set; }
        protected double Amount { get; set; }
        protected DateTime CreationDate { get; set; }


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