using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class Account
    {
        public int IdAccount { get; set; }
        public int IdCustomer { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
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

        public virtual bool CanBeDebited(decimal amount)
        {
            return true;
        }

        public virtual bool CanBeCredited(decimal amount)
        {
            return true;
        }

        
    }
}