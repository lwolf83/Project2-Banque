﻿using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;
using CommandLine.Text;

namespace Project2
{
    class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Activate Verbose Mode")]
        public bool Verbose { get; set; }

        [Option("login", Required = true, HelpText = "Enter the login")]
        public string Login { get; set; }

    }

    [Verb ("createclient", HelpText = "Create a new client.")]
    class CreateClientOptions
    {
        [Option("login", Required = true, HelpText = "Enter the login")]
        public string Login { get; set; }

        [Option("name", Required = false, HelpText = "Enter a name")]
        public string Name { get; set; }

        [Option("location", Required = false, HelpText = "Enter a location.")]
        public string Location { get; set; }
    }

    [Verb("createaccount", HelpText = "Create a new account.")]
    class CreateAccountOptions
    {
        [Option('s', "sa", Required = false, HelpText = "Create a savings account")]
        public bool SavingsAccount { get; set; }

        [Option('c', "ca", Required = false, HelpText = "Create a checking account")]
        public string CheckingAccount { get; set; }

    }

    [Verb("listaccount", HelpText = "List your accounts.")]
    class ListAccountOptions
    {
        //on pourra après ajouter en option des entrées de dates
    }

    [Verb("showinfo", HelpText = "Show your account's informations.")]
    class ShowInfoOptions
    {
        [Value(0)]
        public string AccountId { get; set; }
    }

     class DoTransferOptions
    {
        [Value(0)]
        public double AmountToTransfer { get; set; }

        [Value(1)]
        public string AccountIdOrigin { get; set; }

        [Value(2)]
        public string AccountIdDestination { get; set; }

    }

    [Verb("instant", HelpText = "Do an instant transfer .")]
    class DoInstantTransferOptions : DoTransferOptions
    {
        
    }

    [Verb("deffered", HelpText = "Do a deffered transfer .")]
    class DoDefferedTransferOptions : DoTransferOptions
    {
        [Value(3)]
        public string DefferedDate { get; set; }
    }

    [Verb("permanent", HelpText = "Do a permanent transfer .")]
    class DoPermanentTransferOptions : DoTransferOptions
    {
        [Value(4)]
        public string StartDate { get; set; }

        [Value(5)]
        public string EndDate { get; set; }

        [Value(6)]
        public int Periodicity { get; set; }

    }
}