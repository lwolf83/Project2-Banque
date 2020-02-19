using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    abstract class AccountFactory
    {
        public static Account Create(string type)
        {
            if (type == "SA")
            {
                return new SavingsAccount();
            }
            else if (type == "CA")
            {
                return new CheckingAccount();
            }
            else
            {
                throw new ArgumentException("There is no account for this type. Please enter a valid type of account!");
            }
        }
    }
}
