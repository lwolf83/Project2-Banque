using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JELListener
{
    static class Logger
    {
        public static StreamWriter Stream { get; set; }

        public static void Debug(String message)
        {
            SendMessage("[DEBUG]: " + message);
        }

        public static void Info(String message)
        {
            SendMessage("[INFO]: " + message);
        }

        public static void Error(String message)
        {
            SendMessage("[ERROR]: " + message);
        }

        private static void SendMessage(String message)
        {
            String formattedMessage = String.Format("{0} - {1}", DateTime.Now, message);
            Stream.WriteLine(formattedMessage);
            Stream.Flush();
        }
    }
}
