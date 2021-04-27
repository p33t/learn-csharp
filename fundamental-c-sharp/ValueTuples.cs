using System;
using System.Diagnostics;

namespace fundamental_c_sharp
{
    public static class ValueTuples
    {
        public static void Demo()
        {
            Console.WriteLine("ValueTuples...");
            var t2 = ValueTuple.Create("one", 1);
            Trace.Assert(t2.Item1 == "one");
            Trace.Assert(t2.Item2 == 1);
            Trace.Assert(t2.ToString() == "(one, 1)");
        }
    }
}