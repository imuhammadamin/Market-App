using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_App.Extensions
{
    public static class Capitalizer
    {
        public static string Capitalize(this string text)
        {
            text = char.ToUpper(text[0]) + text.Substring(1);

            return text;
        }
    }
}
