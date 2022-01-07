using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Market_App.Enums;
using Market_App.IRepository;
using Market_App.Models;
using Market_App.Registration;
using Market_App.Service;

namespace Market_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Regist execute = new Regist();
            execute.Menu();

        }
   
    }
}
