using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class Account
    {
        public int IdClient { get; set; }
        public string AccountIdentifier { get; set; }
        public double Amount { get; set; }
        public DateTime CreationDate { get; set; }


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