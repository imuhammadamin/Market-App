using System;
using System.Threading;

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
