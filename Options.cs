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

        [Option('a', "sa", Required = false, HelpText = "Create a saving account")]
        public bool createSavingAccount { get; set; }

        [Option("login", Required = true, HelpText = "Enter the login")]
        public IEnumerable<string> GetLogin { get; set; }

        [Option('c', "newclient", Required = false, HelpText = "Create a client")]
        public string CreateNewClient { get; set; }
    }

}
