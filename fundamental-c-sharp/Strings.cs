using System;
using System.Diagnostics;
using System.Linq;

namespace fundamental_c_sharp;

public class Strings
{
    public static void Demo()
    {
        Console.WriteLine("Strings ===========================");
        Trace.Assert("hello".Equals("HellO", StringComparison.OrdinalIgnoreCase));
        
        // Note: String.HashCode() is different every time !
        var bytes = BitConverter.GetBytes(45612390);
        var hex = BitConverter.ToString(bytes);
        Trace.Assert("66-FD-B7-02".Equals(hex));

        var actualJoin = string.Join("-", new[] { "a", null, "b", "c", null }.Where(s => s != null));
        Trace.Assert("a-b-c".Equals(actualJoin));
    }
}