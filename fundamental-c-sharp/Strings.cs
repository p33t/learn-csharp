using System;
using System.Diagnostics;

namespace fundamental_c_sharp;

public class Strings
{
    public static void Demo()
    {
        Console.WriteLine("Strings ===========================");
        Trace.Assert("hello".Equals("HellO", StringComparison.OrdinalIgnoreCase));
    }
}