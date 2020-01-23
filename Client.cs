using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class Client
    {
        private int _idClient;
        private string _name;
        private string _login;
        private string _password;
        private string _location;
        private List<Account> _accounts;

        public Client(string login)
        {
            _login = login;
        }

        public bool IsLoginExist(string login)
        {
            return true;
        }




        public bool IsAuthorizedClient()
        {
            return true;
        }

        public int GetIdClient()
        {
            return _idClient;
        }

        public void SetIdClient(int idClient)
        {
            _idClient = idClient;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public string GetLogin()
        {
            return _login;
        }

        public void SetLogin(string login)
        {
            _login = login;
        }

        public string GetPassword()
        {
            return _login;
        }

        public void SetPassword(string password)
        {
            _password = password;
        }

        public string GetLocation()
        {
            return _login;
        }

        public void SetLocation(string location)
        {
            _location = location;
        }

        public List<Account> GetAccounts()
        {
            return _accounts;
        }

        public void SetAccounts(List<Account> accounts)
        {
            _accounts = accounts;
        }

        
        public void ClientCreation()
        { 
                IO.DisplayInformation("Creation of a new client");
                Client newClient = new Client(Program.opts.CreateNewClient);
                if (newClient.IsClientExisting())
                {
                    IO.DisplayWarning("This login already exists, we cannot create it", Program.opts.Verbose);
                }
                else
                {
                    IO.SaveDB(newClient);
                }
        }


        public bool IsClientExisting()
        { // vérifie dans la base de données si le client existe en fonction de son login
            return true;    
        }

    }
}