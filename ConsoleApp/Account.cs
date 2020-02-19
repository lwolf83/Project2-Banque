using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    abstract class Account
    {
        public int IdAccount { get; set; }
        public  int IdCustomer { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public bool IsDebitAuthorized { get; set; }
        public DateTime CreationDate { get; set; }
        public abstract decimal Overdraft { get; }
        public abstract decimal Ceiling { get;}
        public abstract double SavingsRate { get;}

        public Account (string accountIdentifier, decimal amount, int idClient)
        {
            AccountNumber = accountIdentifier;
            Amount = amount;
            IdCustomer = idClient;
        }
        public Account()
        {
        }

        public abstract void Debit();

        public abstract void Credit();

        public abstract bool CanBeDebited(decimal amountToTransfer, Account accountDestination);

        public abstract bool CanBeCredited(decimal amountToTransfer);

        public abstract bool IsAuthorizeCustomerToCredit();

        public abstract bool isDebitAuthorized(Account accountDestination);
    }
}