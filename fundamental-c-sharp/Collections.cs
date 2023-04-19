using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace fundamental_c_sharp
{
    public class Collections
    {
        public static void Demo()
        {
            Console.WriteLine("Collections ===========================");

            var colours = new List<string> { "brown", "blue", "grey", "green" };
            var byFirstLetter = colours.GroupBy(s => s[0]).ToDictionary(grp => grp.Key, grp => grp.ToList());
            Trace.Assert(byFirstLetter.ContainsKey('b'));
            Trace.Assert(byFirstLetter.ContainsKey('g'));
            Trace.Assert(!byFirstLetter.ContainsKey('x'));
            Trace.Assert(byFirstLetter['b'].Count == 2);
            Trace.Assert(byFirstLetter['b'].Contains("brown"));
        }
    }
}