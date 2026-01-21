using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Final.NetCore
{
    public static class MyExtension
    {
        public static void FullWidthLine(this string input)
        {
            string fullWidthLine = new string('=', Console.WindowWidth);
            Console.WriteLine(fullWidthLine);
            Console.WriteLine(input + " ↓");
            Console.WriteLine(fullWidthLine);
        }
        public static bool IsValidAzerbaijaniPhone(this string phoneNo)
        {
            string pattern = @"^\+994(50|55|70|77|99|10)\d{7}$";
            return Regex.IsMatch(phoneNo, pattern);
        }
    }
}
