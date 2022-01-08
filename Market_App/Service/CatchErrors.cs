using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Market_App.Service
{
    internal class CatchErrors
    {
        public static void InputError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nInput error! Please try again.");
            Console.ForegroundColor = ConsoleColor.White;

            Thread.Sleep(1500);
        }
    }
}
