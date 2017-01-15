using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsAtWar.Server.Host.Tools
{
    class ExceptionHadler
    {
        public static void ErrorMessage()
        {
            Console.WriteLine("Something went wrong...");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
