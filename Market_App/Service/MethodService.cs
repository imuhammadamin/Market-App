using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_App.Service
{
    internal class MethodService
    {
        public static string GetUserPath(Guid Id)
        {
            return Path.Combine(Constants.UserDbPath, Id.ToString() + ".txt");
        }
    }
}
