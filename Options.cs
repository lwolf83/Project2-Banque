using System;
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

        [Option('c', "newclient", Required = false, HelpText = "Create a client")]
        public string CreateNewClient { get; set; }

        [Option('a', "newaccount", Required = false, HelpText = "Create an account")]
        public string CreateNewAccount { get; set; }

        [Option('l', "listaccount", Required = false, HelpText = "Display account list")]
        public bool ListAccount { get; set; }


    }

}
