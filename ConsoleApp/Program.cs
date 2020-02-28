using CommandLine;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Numerics;

namespace Project2
{
    class Program
    {
        public static Customer currentCustomer;
        public static bool Verbose { set; get;}

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;

            Parser.Default.ParseArguments<LoginOptions, CreateCustomerOptions, CreateAccountOptions, ListAccountOptions, TransactionListOptions, DoDefferedTransferOptions, DoInstantTransferOptions, DoPermanentTransferOptions, Export>(args)
                .WithParsed<LoginOptions>(RunLoginCommand)
                .WithParsed<CreateCustomerOptions>(RunCreateCustomerCommand)
                .WithParsed<CreateAccountOptions>(RunCreateAccountCommand)
                .WithParsed<ListAccountOptions>(RunListAccountCommand)
                .WithParsed<TransactionListOptions>(RunListTransactionCommand)
                .WithParsed<DoDefferedTransferOptions>(RunDefferedTransferCommand)
                .WithParsed<DoInstantTransferOptions>(RunInstantTransferCommand)
                .WithParsed<DoPermanentTransferOptions>(RunPermanentTransferCommand)
                .WithParsed<Export>(RunExportCommand);
        }

        
        static void RunLoginCommand(LoginOptions opts)
        {
            if (Customer.IsCustomerExisting(opts.Login))
            {
                currentCustomer = DBQuery.getCustomerFromDbWhereLogin(opts.Login);
                currentCustomer.Accounts = DBQuery.GetAccountsCustomer(currentCustomer.IdCustomer);
                string password = " ";
                int i = 0;
                do
                {
                    Console.WriteLine("Please type in your password");
                    password = IO.PromptPassword();
                    password = Sha256Tools.GetHash(password);
                    i++;
                }
                while ((password != currentCustomer.Password) && (i <= 2));
                if (password == currentCustomer.Password)
                {
                    IO.DisplayInformation("You are connected!");
                }
                else
                {
                    IO.DisplayWarning("Too many attempts, please try again later!");
                    Environment.Exit(1);
                }
            }
            else
            {
                IO.DisplayWarning("Your account doesn't exist!");
            }
        }
            
        static void RunDefferedTransferCommand(DoDefferedTransferOptions opts)
        {
            if (Customer.IsAccountOwner(currentCustomer.IdCustomer, opts.AccountIdOrigin))
            {
                AbstractAccount accountOrigin = DBQuery.GetAccountFromDB(opts.AccountIdOrigin);
                AbstractAccount accountDestination = DBQuery.GetAccountFromDB(opts.AccountIdDestination);
                if (accountOrigin.CanBeDebited(opts.AmountToTransfer, accountDestination) && accountDestination.CanBeCredited(opts.AmountToTransfer))
                {
                    currentCustomer.MakeNewTransaction(opts.AmountToTransfer, accountOrigin, accountDestination, DateTime.Parse(opts.DefferedDate));
                }
            }
            else
            {
                IO.DisplayWarning("You can't make this deferred transfer, you aren't the account owner!");
            }
        }

        static void RunInstantTransferCommand(DoInstantTransferOptions opts)
        {
            if(Customer.IsAccountOwner(currentCustomer.IdCustomer, opts.AccountIdOrigin))
            {
                AbstractAccount accountOrigin = DBQuery.GetAccountFromDB(opts.AccountIdOrigin);
                AbstractAccount accountDestination = DBQuery.GetAccountFromDB(opts.AccountIdDestination);
                if(accountOrigin.CanBeDebited(opts.AmountToTransfer, accountDestination) && accountOrigin.isMoneyEnough(opts.AmountToTransfer) && accountDestination.CanBeCredited(opts.AmountToTransfer) && accountDestination.isTransferNotReachingCeiling(opts.AmountToTransfer))
                {
                    currentCustomer.MakeNewTransaction(opts.AmountToTransfer, accountOrigin, accountDestination);
                }
               
            }
            else
            {
                IO.DisplayWarning("You can't make this instant transfer, you aren't the account owner!");
            }
        }

        static void RunPermanentTransferCommand(DoPermanentTransferOptions opts)
        {
            if (Customer.IsAccountOwner(currentCustomer.IdCustomer, opts.AccountIdOrigin))
            {
                AbstractAccount accountOrigin = DBQuery.GetAccountFromDB(opts.AccountIdOrigin);
                AbstractAccount accountDestination = DBQuery.GetAccountFromDB(opts.AccountIdDestination);
                if (accountOrigin.CanBeDebited(opts.AmountToTransfer, accountDestination) && accountDestination.CanBeCredited(opts.AmountToTransfer))
                {
                    currentCustomer.MakeNewTransaction(opts.AmountToTransfer, accountOrigin, accountDestination, DateTime.Parse(opts.StartDate), DateTime.Parse(opts.EndDate), opts.Periodicity);
                }
            }
            else
            {
                IO.DisplayWarning("You can't make this deferred transfer, you aren't the account owner!");
            }
        }

        static void RunCreateCustomerCommand(CreateCustomerOptions opts)
        {  
            if (Customer.IsCustomerExisting(opts.Login))
            {
                IO.DisplayWarning("Your account already exists!");
            }
            else
            {
                string password = SetUpPasswordFromKeyboard();
                password = Sha256Tools.GetHash(password);
                DBQuery.SaveNewCustomerInDb(opts.Name, opts.Login, password, opts.Location);
                Console.WriteLine("Your account has been created.");
            }
        }

        private static string SetUpPasswordFromKeyboard()
        {
            string password;
            bool isComplexPassword;
            int inputError = 0;
            do
            {
                Console.WriteLine("Please, set your password : ");
                password = IO.PromptPassword();
                isComplexPassword = IO.IsComplexPassword(password);

                if (!isComplexPassword)
                {
                    WrongPasswordMessage(password, inputError);
                }
                inputError++;
            }
            while (!isComplexPassword);
            Console.WriteLine("Your password is valid.");
            return password;
        }

        static bool WrongPasswordMessage(string password, int nbErreurSaisie)
        {
            if (nbErreurSaisie < 3)
            {
                IO.DisplayWarning("Your password has not a sufficient complexity! Please, Put a valid password.");
            }
            if (nbErreurSaisie >= 3)
            {
                IO.DisplayWarning("Your password must have at least 8 characters, lower and upper letters, " +
                    "at least one number and one special character!");
            }
            return true;
        }

        static void RunCreateAccountCommand(CreateAccountOptions opts)
        {
            if (Customer.IsCustomerExisting(opts.Login))
            {
                if (opts.AccountType == "ca")
                {
                    currentCustomer.AddCheckingAccount();
                }
                else if (opts.AccountType == "sa")
                {
                    currentCustomer.AddSavingAccount();
                }
                else
                {
                    IO.DisplayWarning("You don't enter a valid type of account!");
                }
            }
            else
            {
                IO.DisplayWarning("Cannot create an account on a customer not existing!");
            }
        }

        static void RunListAccountCommand(ListAccountOptions opts)
        {
            List<AbstractAccount> AccountsList = Customer.GetAccountList(currentCustomer.IdCustomer);
            IO.DisplayAccountList(currentCustomer, AccountsList);
        }
        static void RunListTransactionCommand(TransactionListOptions opts)
        {
            List<AbstractTransaction> transactionList = DBQuery.GetTransactionList(opts.AccountNumber);
            IO.DisplayTransactionList(transactionList);
        }

        static void RunExportCommand(Export opts)
        {
            string dateExport = DateTime.Now.ToString("yyyyMMddHmmss");
            if (opts.ExportAccounts)
            {
                using (var writer = new StreamWriter("./" + dateExport + "AccountCsv.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.RegisterClassMap<CSV>();
                    csv.WriteRecords(currentCustomer.Accounts);
                }
                Console.WriteLine("CSV File is created.");
            }
            else if (opts.ExportTransactions)
            {
                using (var writer = new StreamWriter("./" + dateExport + "TransactionCsv.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    List<AbstractTransaction> transactionList = DBQuery.GetTransactionList(opts.AccountNumber);
                    csv.Configuration.RegisterClassMap<CSV>();
                    csv.WriteRecords(transactionList);
                }
                Console.WriteLine("CSV File is created.");
            }
            else if (opts.ExportTransfers)
            {
                using (var writer = new StreamWriter("./" + dateExport + "TransferCsv.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    List<TransferMoney> transfertMoneyList = DBQuery.GetTransfertList(Convert.ToString(opts.AccountNumber));
                    csv.Configuration.RegisterClassMap<CSV>();
                    csv.WriteRecords(transfertMoneyList);
                }
                Console.WriteLine("CSV File is created.");
            }
            else
            {
                IO.DisplayWarning("Cannot create your CSV File please choose an option!");
            }
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            //handle errors
        }
    }
}
