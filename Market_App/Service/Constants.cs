using Market_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_App.Service
{
    internal class Constants
    {
        public static readonly string ProductsDbPath = @"..\..\..\Database\ProductsData\ProductsDB.txt";
        public static readonly string UsersDbPath = @"..\..\..\Database\UsersData";
        public static readonly string HistoriesPath = @"..\..\..\Database\History.json";
    }
}
