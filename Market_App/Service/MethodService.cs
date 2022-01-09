using Market_App.IRepository;
using Market_App.Models;
using Market_App.Registration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace Market_App.Service
{
    internal class MethodService
    {
        public static string HashPassword(string password)
        {
            string pass = "";
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            foreach (byte b in tmpHash)
            {
                pass += b.ToString();
            }

            return pass;
        }
    }
}
