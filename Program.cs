using CommandLine;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Project2
{
    class Program
    {
        public static Options opts;
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
                Client newClient = IO.GetCustomerFromLogin("laure");
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
                .ParseArguments<VerboseOptions, LoginOptions, CreateClientOptions, CreateAccountOptions, ListAccountOptions, 
                ShowInfoOptions, DoDefferedTransferOptions, DoInstantTransferOptions, DoPermanentTransferOptions>(args)
                .MapResult(
                (VerboseOptions opts) => RunVerboseCommand(opts),
                (LoginOptions opts) => RunLoginCommand(opts),
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

        static int RunVerboseCommand(VerboseOptions options)
        {
            return 1;
        }

        static int RunLoginCommand(LoginOptions opts)
        {
            Client existingClient = new Client("Jean", "jeanbarth", "azerty", "strasbourg");
            Client clientWhoWantsToLogIn = new Client();
            string passwordExisting;
            if (clientWhoWantsToLogIn.IsClientExisting())
            {
                passwordExisting = clientWhoWantsToLogIn.Password;
            }
            else
            {
                return 1;
            }
            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine();
            int i = 0;
            while ((password != passwordExisting) || (i < 2))
            {
                Console.WriteLine("Wrong password, please try again");
                password = Console.ReadLine();
                i++;
            }
            if ((i == 2) && (password != passwordExisting))
            {
                return 1;
            }
            else
            {
                Console.WriteLine("You entered 3 times a wrong password, try again in 10 minutes");
                return 0;
            }

            //vérification que le client existe
            //Non, on quitte
            //oui, on recupère le mot de passe dans la db getPasswordFromUser(string user) {return "azert"};
            //tant que le mot de passe n'est pas valide le redemander. A 3 essais faux afficher que le mot de passe n'est pas bon et quitter.

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
