using CommandLine;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Project2
{
    class Program
    {
       
        public static bool Verbose { set; get;}
        public static SqlConnection sqlConnexion;

        static void Main(string[] args)
        {
            Verbose = true;
            DBUtils.GetDBConnection();
            try
            {
                DBQuery.getCustomerFromDbWhereLogin("jeanbarth");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }

            CommandLine.Parser.Default
                .ParseArguments<CreateClientOptions, CreateAccountOptions, ListAccountOptions,
                ShowInfoOptions, DoDefferedTransferOptions, DoInstantTransferOptions, DoPermanentTransferOptions>(args)
                .MapResult(
                (CreateClientOptions opts) => RunCreateClientCommand(opts),
                (CreateAccountOptions opts) => RunCreateAccountCommand(opts),
                (ListAccountOptions opts) => RunListAccountCommand(opts),
                (ShowInfoOptions opts) => RunShowInfoCommand(opts),
                (DoDefferedTransferOptions opts) => RunDefferedTransferCommand(opts),
                (DoInstantTransferOptions opts) => RunInstantTransferCommand(opts),
                (DoPermanentTransferOptions opts) => RunPermanentTransferCommand(opts),
                (parserErrors) => 1
                );

            sqlConnexion.Close();
            sqlConnexion.Dispose();
        }


        static int Connection(Options opts)
        {
            Client currentCustomer = new Client(opts.Login);
            string passwordInDB;
            if (currentCustomer.IsCustomerExisting(opts.Login))
            {
                currentCustomer = DBQuery.getCustomerFromDbWhereLogin(opts.Login);
                passwordInDB = currentCustomer.Password;
            }
            else
            {
                return 1;
            }

            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine();
            int i = 0;
            if (password == passwordInDB)
            {
                Console.WriteLine("You are connected !");
                return 0;
            }
            else if (currentCustomer.PasswordDifferentFromPasswordInDB(opts.Login, password))
            {
                return 1;
            }
            else
            {
                Console.WriteLine("You are connected !");
                return 0;
            }

            //vérification que le client existe
            //Non, on quitte
            //oui, on recupère le mot de passe dans la db getPasswordFromUser(string user) {return "azert"};
            //tant que le mot de passe n'est pas valide le redemander. A 3 essais faux afficher que le mot de passe n'est pas bon et quitter.
        }
            
        static int RunDefferedTransferCommand(DoDefferedTransferOptions opts)
        {
            Connection(opts);
            return 1;
        }

        static int RunInstantTransferCommand(DoInstantTransferOptions opts)
        {
            Connection(opts);
            return 1;
        }

        static int RunPermanentTransferCommand(DoPermanentTransferOptions opts)
        {
            Connection(opts);
            return 1;
        }

        static int RunCreateClientCommand(CreateClientOptions opts)
        {
            Connection(opts);
            return 1;
        }

        static int RunCreateAccountCommand(CreateAccountOptions opts)
        {
            Connection(opts);
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
        {/*
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
            }*/
        }
    }
}
