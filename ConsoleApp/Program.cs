using CommandLine;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;

namespace Project2
{
    class Program
    {
        public static Customer currentCustomer;
        public static bool Verbose { set; get;}

        static void Main(string[] args)
        {
            
            CommandLine.Parser.Default
                .ParseArguments<CreateCustomerOptions, CreateAccountOptions, ListAccountOptions,
                ShowInfoOptions, DoDefferedTransferOptions, DoInstantTransferOptions, DoPermanentTransferOptions>(args)
                .MapResult(
                (CreateCustomerOptions opts) => RunCreateCustomerCommand(opts),
                (CreateAccountOptions opts) => RunCreateAccountCommand(opts),
                (ListAccountOptions opts) => RunListAccountCommand(opts),
                (ShowInfoOptions opts) => RunShowInfoCommand(opts),
                (DoDefferedTransferOptions opts) => RunDefferedTransferCommand(opts),
                (DoInstantTransferOptions opts) => RunInstantTransferCommand(opts),
                (DoPermanentTransferOptions opts) => RunPermanentTransferCommand(opts),
                (Export opts) => RunExportCommand(opts),
                (parserErrors) => 1
                );

        }

        static int Connection(Options opts)
        {
            currentCustomer = new Customer(opts.Login);
            if (currentCustomer.IsCustomerExisting(opts.Login))
            {
                currentCustomer = DBQuery.getCustomerFromDbWhereLogin(opts.Login);
                string password = " ";
                int i = 0;
                do
                {
                    Console.WriteLine("Please type in your password");
                    password = Console.ReadLine();
                    i++;
                }
                while ((password != currentCustomer.Password) && (i <= 2));
                
                if (password == currentCustomer.Password)
                {
                    IO.DisplayInformation("You are connected!");
                    return 0;
                }
                else
                {
                    IO.DisplayWarning("Too many attempts, please try again later!");
                    return 1;
                }
                
            }
            IO.DisplayWarning("Your account doesn't exist!");
            return 1;

        }
            
        static int RunDefferedTransferCommand(DoDefferedTransferOptions opts)
        {
            Connection(opts);
            if (currentCustomer.IsAccountOwner(opts.AccountIdOrigin))
            {
                Account accountOrigin = DBQuery.GetAccountFromDB(opts.AccountIdOrigin);
                Account accountDestination = DBQuery.GetAccountFromDB(opts.AccountIdDestination);
                // vérifier que l'on peut retirer de l'argent du compte

                if (accountOrigin.CanBeDebited(opts.AmountToTransfer, accountDestination) && accountDestination.CanBeCredited(opts.AmountToTransfer))
                {
                    currentCustomer.MakeNewDefferedTransaction(opts.AmountToTransfer, accountOrigin, accountDestination, DateTime.Parse( opts.DefferedDate));
                }
            }
            return 1;
        }

        static int RunInstantTransferCommand(DoInstantTransferOptions opts)
        {
            Connection(opts);
            // définir le type de compte d'origine
            // définir le type de compte d'arrivée
            // vérifier si le compte de départ appartient bien au client
            if(currentCustomer.IsAccountOwner(opts.AccountIdOrigin))
            {
                Account accountOrigin = DBQuery.GetAccountFromDB(opts.AccountIdOrigin);
                Account accountDestination = DBQuery.GetAccountFromDB(opts.AccountIdDestination);
                // vérifier que l'on peut retirer de l'argent du compte

                if(accountOrigin.CanBeDebited(opts.AmountToTransfer, accountDestination) && accountDestination.CanBeCredited(opts.AmountToTransfer))
                {
                    currentCustomer.MakeNewInstantTransaction(opts.AmountToTransfer, accountOrigin, accountDestination);
                }
                // vérifier que l'on peut créditer le compte d'arrivée
                // si les deux sont ok on crée la transaction
                // on crée la transaction
            }
            return 1;
        }

        static int RunPermanentTransferCommand(DoPermanentTransferOptions opts)
        {
            Connection(opts);

            if (currentCustomer.IsAccountOwner(opts.AccountIdOrigin))
            {
                Account accountOrigin = DBQuery.GetAccountFromDB(opts.AccountIdOrigin);
                Account accountDestination = DBQuery.GetAccountFromDB(opts.AccountIdDestination);
                // vérifier que l'on peut retirer de l'argent du compte

               
                    currentCustomer.MakeNewPermanentTransaction(opts.AmountToTransfer, accountOrigin, accountDestination, opts.StartDate, opts.EndDate, opts.Periodicity);
                
                // vérifier que l'on peut créditer le compte d'arrivée
                // si les deux sont ok on crée la transaction
                // on crée la transaction
            }

            return 1;
        }

        static int RunCreateCustomerCommand(CreateCustomerOptions opts)
        {
            

            currentCustomer = new Customer(opts.Login);
            if (currentCustomer.IsCustomerExisting(opts.Login))
            {
                Console.WriteLine("Your account already exists.");
                return 0;
            }
            else
            {
                string password = SetUpPasswordFromKeyboard();

                IO.DisplayInformation("Your password is valid.");

                //sauvegarder le client            
                DBQuery.SaveNewCustomerInDb(opts.Name, opts.Login, password, opts.Location);
                return 1;
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
                password = Console.ReadLine();
                isComplexPassword = Customer.IsComplexPassword(password);

                if (!isComplexPassword)
                {
                    WrongPasswordMessage(password, inputError);
                }
                inputError++;
            }
            while (!isComplexPassword);
            return password;
        }

        static bool WrongPasswordMessage(string password, int nbErreurSaisie)
        {
            if (nbErreurSaisie < 3)
            {
                Console.WriteLine("Your password has not a sufficient complexity. Please, Put a valid password.");
            }
            if (nbErreurSaisie >= 3)
            {
                Console.WriteLine("Your password must have at least 8 characters, lower and upper letters, " +
                    "at least one number and one special character.");
            }
            return true;
        }

        static int RunCreateAccountCommand(CreateAccountOptions opts)
        {
            Connection(opts);
            if(opts.CheckingAccount)
            {
                currentCustomer.AddCheckingAccount();
            }
            if(opts.SavingsAccount)
            {
                currentCustomer.AddSavingAccount();
            }


            return 1;
        }

        static int RunListAccountCommand(ListAccountOptions opts)
        {
            Connection(opts);
            return 1;
        }

        static int RunShowInfoCommand(ShowInfoOptions opts)
        {
            Connection(opts);
            return 1;
        }

        static int RunExportCommand(Export opts)
        {
            Connection(opts);
            return 1;
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            //handle errors
        }
        
    }
}
