using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Transactions;

namespace fundamental_c_sharp
{
    public static class Enumerators
    {
        private static IEnumerator<string> GenerateStrings()
        {
            yield return "one";
            yield return "two";
            yield return "three";
        }
        
        public static void Demo()
        {
            Console.WriteLine("Enumerators ===========================");

            var actual = "";
            for (var ss = GenerateStrings(); ss.MoveNext();)
            {
                actual += "." + ss.Current;
            }
            Trace.Assert(actual.Equals(".one.two.three"));
        }
    }
}