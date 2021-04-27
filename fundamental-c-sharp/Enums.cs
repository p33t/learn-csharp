using System;
using System.Diagnostics;

namespace fundamental_c_sharp
{
    public class Enums
    {
        public enum MyEnum
        {
            First, Second, Third
        }

        [Flags]
        public enum MyFlags
        {
            None =  0,
            Before =  1 << 1,
            After = 1 << 2,
            During = 1 << 3,
        }
        public static void Demo()
        {
            Trace.Assert(MyEnum.First.ToString() == "First");
            Util.WriteLn("Enum ToString() works");

            var beforeAndAfter = MyFlags.Before | MyFlags.After;
            Util.WriteLn($"Expecting Before, After: {beforeAndAfter}");

            Util.WriteLn($"Expecting no overlap (True): {(MyFlags.Before & MyFlags.After) == MyFlags.None}");
            Util.WriteLn($"Expecting overlap (False): {(MyFlags.Before & MyFlags.Before) == MyFlags.None}");
            
            // Looks like 'None' is always considered present
            // Trace.Assert(!beforeAndAfter.HasFlag(MyFlags.None), "Seems like 'None' is always present");
            Trace.Assert(beforeAndAfter.HasFlag(beforeAndAfter), "HasFlag() doesn't like multi-flags");
            var beforeAfterDuring = beforeAndAfter | MyFlags.During;
            Trace.Assert(beforeAfterDuring.HasFlag(beforeAndAfter), "HasFlag() cannot check for multiple flags");
            Trace.Assert(!beforeAndAfter.HasFlag(beforeAfterDuring), "HasFlag() cannot handle multi flags");
        }
    }
}