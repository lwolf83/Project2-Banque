﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;


namespace Project2
{
    class Program
    {
        public static Options opts;
        public static Client currentClient;
        public static SqlConnection conn;


        static void Main(string[] args)
        {
            Console.WriteLine("Getting Connection ...");
            conn = DBUtils.GetDBConnection();

            try
            {
                Console.WriteLine("Openning Connection ...");

                conn.Open();

                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            try
            {
                QueryEmployee(conn);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                // Closez la connexion.
                conn.Close();
                // Éliminez l'objet, libérant les ressources.
                conn.Dispose();
            }
            Console.Read();


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

            // Closez la connexion.
            conn.Close();
            // Éliminez l'objet, libérant les ressources.
            conn.Dispose();
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

            List<Account> accountList = currentClient.GetAccountList(); //recuperation liste
            IO.DisplayAccountList(currentClient, accountList); //afficher liste

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
