using CommandLine;
using System;
using System.Collections.Generic;

namespace Project2
{
    class Program
    {
        public static Options opts;



        static void Main(string[] args)
        {

            CommandLine.Parser.Default
                .ParseArguments<Options, CreateClientOptions, CreateAccountOptions, ListAccountOptions, 
                ShowInfoOptions, DoDefferedTransferOptions, DoInstantTransferOptions, DoPermanentTransferOptions>(args)
                .MapResult(
                (Options opts) => RunCommand(opts),
                (CreateClientOptions opts) => RunCreateClientCommand(opts),
                (CreateAccountOptions opts) => RunCreateAccountCommand(opts),
                (ListAccountOptions opts) => RunListAccountCommand(opts),
                (ShowInfoOptions opts) => RunShowInfoCommand(opts),
                (DoDefferedTransferOptions opts) => RunDefferedTransferCommand(opts),
                (DoInstantTransferOptions opts) => RunInstantTransferCommand(opts),
                (DoPermanentTransferOptions opts) => RunPermanentTransferCommand(opts),

                (parserErrors) => 1
                );

        }

        static int RunCommand(Options options)
        {
            opts = options;
            string action;
            

            return 1;
        }

        static int RunDefferedTransferCommand(DoDefferedTransferOptions opts)
        {
            return 1;
        }

        static int RunInstantTransferCommand(DoInstantTransferOptions opts)
        {
            return 1;
        }

        static int RunPermanentTransferCommand(DoPermanentTransferOptions opts)
        {
            return 1;
        }

        static int RunCreateClientCommand(CreateClientOptions opts)
        {
            return 1;
        }

        static int RunCreateAccountCommand(CreateAccountOptions opts)
        {
            return 1;
        }

        static int RunListAccountCommand(ListAccountOptions opts)
        {
            return 1;
        }

        static int RunShowInfoCommand(ShowInfoOptions opts)
        {
            return 1;
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            //handle errors
        }

        /*public static void ClientCreation()
        {
            IO.DisplayInformation("Creation of a new client");
            Client newClient = new Client(Program.opts.CreateNewClient);
            if (newClient.IsClientExisting())
            {
                IO.DisplayWarning("This login already exists, we cannot create it.");
            }
            else
            {
                IO.SaveDB(newClient);
                IO.DisplayInformation("New client saved in the database.");
            }
        }*/

        public static void SavingsAccountCreation()
        {
            IO.DisplayInformation("Creation of a savings account");
            Client client = new Client(Program.opts.Login);
            if (client.IsClientExisting())
            {
                IO.DisplayInformation("New account saved in the database.");
                SavingsAccount newAccount = new SavingsAccount(client.IdClient);
                List<Account> accountList=client.Accounts;
                accountList.Add(newAccount);
                IO.SaveDB(client);
                IO.SaveDB(newAccount);
            }
            else
            {
                IO.DisplayWarning("This client doesn't exist.");
            }
           
        }
    }
}
