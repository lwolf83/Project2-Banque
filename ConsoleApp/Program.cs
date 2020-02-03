﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Project2
{
    class Program
    {
        public static Options opts;
        public static bool Verbose { set; get;}
        public static SqlConnection sqlConnexion;

        static void Main(string[] args)
        {
            Verbose = true;
            DBUtils.GetDBConnection();
            try
            {
                DBQuery.getCustomerFromDbWhereLogin("lwolf");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }

            
            CommandLine.Parser.Default
                .ParseArguments<Options, CreateClientOptions, CreateAccountOptions, ListAccountOptions, 
                ShowInfoOptions, DoDefferedTransferOptions, DoInstantTransferOptions, DoPermanentTransferOptions>(args)
                .MapResult(
                (Options opts) => RunCommand(opts),
                (CreateClientOptions opts) => RunCreateCustomerCommand(opts),
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

        static int RunCommand(Options options)
        {
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

        static int RunCreateCustomerCommand(CreateClientOptions opts)
        {
            //creation du client
            //demander le mot de passe au client
            string password;
            bool isComplexPassword;
            int nbErreurSaisie = 0;
            do
            {
                Console.WriteLine("Please, enter your password : ");
                password = Console.ReadLine();
                isComplexPassword = Client.IsComplexPassword(password);

                if (!isComplexPassword)
                {
                    WrongPasswordMessage(password, nbErreurSaisie);
                }
                nbErreurSaisie++;
            }
            while (!isComplexPassword);

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
            Console.WriteLine("Your password is valid.");

            //sauvegarder le client            
            DBQuery.saveNewCustomerInDb(1, opts.Name, opts.Login, password, opts.Location, "20/01/2020");
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