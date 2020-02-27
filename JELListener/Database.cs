﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace JELListener
{
    class Database
    {
        private static Database _instance = null;
        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }

        private SqlConnection _connection = null;

        public void Connect(SqlConnectionStringBuilder builder)
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                throw new Exception("Database already connected");
            }
            _connection.ConnectionString = builder.ConnectionString;
            _connection.Open();
        }

        public IEnumerable<Transaction> GetTransactions(DateTime startDate, DateTime? endDate)
        {
            throw new NotImplementedException("Retourne toutes les transaction avec les comptes associés (Account) et les transferts associés");
        }

        public IEnumerable<Transfer> GetTransfersByTransaction(Transaction transaction)
        {
            throw new NotImplementedException("Not implemented yet");
        }

        public Account GetOriginAccountByTransaction(Transaction transaction)
        {
            throw new NotImplementedException("Récupérer le compte d'origine d'une transaction");
        }

        public Account GetDestinaryAccountByTransaction(Transaction transaction)
        {
            throw new NotImplementedException("Récupérer le compte destinataire");
        }

        public void UpdateTransaction(Transaction transaction)
        {
            throw new NotImplementedException("Mettre à jour la transaction et TOUS ses transfert associés en base");
        }
        
        public void UpdateTransfer(Transfer transfer)
        {
            throw new NotImplementedException("Mettre à jour un transfer en base");
        }

        public void UpdateAccount(Account account)
        {
            throw new NotImplementedException("Mettre à jour le compte en fonction du paramètre account");
        }

    }
}