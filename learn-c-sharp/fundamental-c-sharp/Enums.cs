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
            None =  0b_0000,
            Before =  0b_0001,
            After = 0b_0010,
        }
        public static void Demo()
        {
            Trace.Assert(MyEnum.First.ToString() == "First");
            Util.WriteLn("Enum ToString() works");

            var both = MyFlags.Before | MyFlags.After;
            Util.WriteLn($"Expecting Before, After: {both}");
        }
    }
}