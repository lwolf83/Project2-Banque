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



        public void CreateClient()
        {
            
        }
        

    }
}