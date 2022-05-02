using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace fundamental_c_sharp
{
    public class RegExps
    {
        public static void Demo()
        {
            Console.WriteLine("RegExps ===========================");
            var pattern = @"^[A-Za-z_-]{1,50}$";
            var regExp = new Regex(pattern);
            
            Trace.Assert(regExp.IsMatch("") == false);
            Trace.Assert(regExp.IsMatch("a b") == false);
            Trace.Assert(regExp.IsMatch("a#") == false);
            Trace.Assert(regExp.IsMatch("#a") == false);
            Trace.Assert(regExp.IsMatch("abc"));
        }
    }
}