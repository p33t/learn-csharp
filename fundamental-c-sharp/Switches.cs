using System;
using System.Diagnostics;
using System.Transactions;

namespace fundamental_c_sharp
{
    public static class Switches
    {
        public static void Demo()
        {
            Console.WriteLine("Switches ===========================");

            static string Run(int? i) => i switch
            {
                null => "none",
                1 => "one",
                _ => i!.Value.ToString()
            };

            Trace.Assert("none" == Run(null));
            Trace.Assert("10" == Run(10));
            Trace.Assert("one" == Run(1));
        }
    }
}