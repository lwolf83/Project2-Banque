using System;
using System.Collections.Generic;

namespace Project2
{
    class Program
    {
        static void Main(string[] args)
        {
            string action = IO.getAction(args);
            /*
             * jeu de test jusqu'à possibilité de lecteur en db 
             */
            Client currentUser = new Client();
            currentUser.SetIdClient(1);
            currentUser.SetLogin("john doe");
            currentUser.SetPassword("azerty");

            SavingsAccount userSavingsAccount = new SavingsAccount();
            userSavingsAccount.SetAccountIdentifier("CC0001");
            userSavingsAccount.SetAmount(1000);
            userSavingsAccount.SetIdClient(1);

            CheckingAccount userCheckingAccount = new CheckingAccount();
            List<Account> userAccountsList =  new List<Account>();
            userSavingsAccount.SetAccountIdentifier("CE0001");
            userSavingsAccount.SetAmount(10000);
            userSavingsAccount.SetIdClient(1);

            userAccountsList.Add(userSavingsAccount);
            userAccountsList.Add(userCheckingAccount);
            currentUser.SetAccounts(userAccountsList);
            /*
             *  fin de jeu de test
             */


            if (currentUser.IsAuthorizedClient() && !(action == "createUser"))
            {
                switch(action)
                {
                    case "createAccount":
                        //on créé un nouveau compte en prenant les valeurs données en parametre
                        //récupération de l'ensemble des infos dans args
                        //sauvegarde en db
                        break;
                    case "getAccountList":
                        //recupérer l'ensemble des comptes de l'utilisateur
                        //si presence de l'argument /csv dans args le sauvegarder dans un csv sinon l'afficher à l'écran.
                        break;
                    case "exportAccount":
                        break;
                    case "doWireTransfert":
                        //vérifer que l'ensemble des informations nécessaires sont disponibles
                        //verifier que le compte origine est bien à l'utilisateur
                        //verifier que le compte origine peut bien être débité de la valeur
                        //verifier que le compte destination existe
                        //réaliser le transfert
                        //sauvegarde en db
                        break;
                    default:
                        //l'utilisateur n'a choisi aucune option. On affiche l'erreur et on termine le programme
                        Console.WriteLine("no possible action available");
                        break;

                }

            }
            else if(action == "createUser")
            {
                //on crée un nouvel utilisateur
                //on verifie que le login n'existe deja pas en db (echec si le login existe deja)
                //(optionnal) on verifie que le mot de passe possède une complexité suffisante (8 caractères, 1 maj, 1min, 1 chiffre, 1 caractères spécial dans ,;:!?./%+={([|])}
                //on crée l'utilisateur
                //on le sauvegarde en db
                currentUser.CreateClient();
            }
            else
            {   
                //l'authentification a échoué.
                //on rejette et on termine le programme.
                Console.WriteLine("no possible action available");
            }

        }
    }
}
