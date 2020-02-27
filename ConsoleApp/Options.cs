using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;
using CommandLine.Text;

namespace Project2
{
    class LoginOptions : Options
    {
        [Option('l', "login", Required = true, HelpText = "Enter the login")]
        public string Login { get; set; }
    }

    class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Activate Verbose Mode")]
        public bool Verbose
        {
            set
            {
                Program.Verbose = value;
            }
        }
    }


    [Verb ("createcustomer", HelpText = "Create a new customer.")]
    class CreateCustomerOptions : Options
    {
        [Option('u', "username", Required = true, HelpText = "The customer username")]
        public string Login { get; set; }

        [Option('n',"name", Required = false, HelpText = "Enter a name")]
        public string Name { get; set; }

        [Option('p',"location", Required = false, HelpText = "Enter a location.")]
        public string Location { get; set; }
    }

    [Verb("createaccount", HelpText = "Create a new account.")]
    class CreateAccountOptions : LoginOptions
    {
        [Option('t', "type", Required = false, HelpText = "Choose your type account", MetaValue = "sa/ca")]
        public string AccountType { get; set; }
    }

    [Verb("listaccount", HelpText = "List your accounts.")]
    class ListAccountOptions : LoginOptions
    {
        //on pourra après ajouter en option des entrées de dates
    }

    [Verb("showinfo", HelpText = "Show your account's informations.")]
    class ShowInfoOptions : LoginOptions
    {
        [Option('a', "accounts", Required = false, HelpText = "Export customer's account.")]
        public bool ExportAccounts { get; set; }
    }

     class DoTransferOptions : LoginOptions
    {
        [Option('a', "amount", Required = false, HelpText = "Amount to transfer.")]
        public decimal AmountToTransfer { get; set; }

        [Option('o', "origin", Required = false, HelpText = "Origin account.")]
        public string AccountIdOrigin { get; set; }

        [Option('d', "destination", Required = false, HelpText = "Destination account.")]
        public string AccountIdDestination { get; set; }
    }

    [Verb("instant", HelpText = "Do an instant transfer .")]
    class DoInstantTransferOptions : DoTransferOptions
    {
        
    }

    [Verb("deffered", HelpText = "Do a deffered transfer .")]
    class DoDefferedTransferOptions : DoTransferOptions
    {
        [Option('t', "date", Required = true , HelpText = "Transfer date", MetaValue = "yyyy-mm-dd")]
        public string DefferedDate { get; set; }
    }

    [Verb("permanent", HelpText = "Do a permanent transfer .")]
    class DoPermanentTransferOptions : DoTransferOptions
    {
        [Option('s', "start", Required = true, HelpText = "Starting date of a transaction", MetaValue = "yyyy-mm-dd")]
        public string StartDate { get; set; }

        [Option('e', "end", Required = true, HelpText = "Ending date of a transaction", MetaValue = "yyyy-mm-dd")]
        public string EndDate { get; set; }

        [Option('p', "periodicity", Required = true, HelpText = "Number of days between subsequent transfers", MetaValue = "periodicity")]
        public int Periodicity { get; set; }
    }

    [Verb("csv", HelpText = "Do export of customer's account.")]
    class Export : LoginOptions
    {
        [Option('a', "accounts", Required = false, HelpText = "Export customer's account.")]
        public bool ExportAccounts { get; set; }
    
        [Option('t', "transactions", Required = false, HelpText = "Export customer's transaction.")]
        public bool ExportTransactions { get; set; }

        [Option('f', "transfer", Required = false, HelpText = "Export customer's transfer.")]
        public bool ExportTransfers { get; set; }

        [Option('i', "id", Required = false, HelpText = "Account number", MetaValue = "account-number")]
        public string AccountNumber { get; set; }
    }
}
